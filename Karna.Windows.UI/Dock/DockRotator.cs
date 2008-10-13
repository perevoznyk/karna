//===============================================================================
// Copyright © Serhiy Perevoznyk.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================


using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using Karna.Windows.UI.Attributes;

namespace Karna.Windows.UI.Dock
{
    public enum MoveDirection { Left, Right }

    [KarnaPurpose("Dock")]
    public class DockRotator : CustomDockManager
    {

        private const float minFocalLength = 0.001f;

        [AccessedThroughProperty("UseScale")]
        private bool useScale = true;
        [AccessedThroughProperty("MoveDirection")]
        private MoveDirection moveDirection = MoveDirection.Right;
        [AccessedThroughProperty("Radius")]
        private float radius = 160.0f;
        [AccessedThroughProperty("Speed")]
        private float speed = 1.0f;
        [AccessedThroughProperty("FocalLength")]
        private float focalLength = 150.0f;
        [AccessedThroughProperty("OffsetY")]
        private int offsetY = 50;


        public DockRotator()
        {
            items = new List<DockItem>();
        }


        public override void UpdateDockInfo()
        {
            UpdateCoordinates();
        }

        [DefaultValue(160.0f)]
        public float Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
            }
        }

        [DefaultValue(1.0f)]
        public float Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }

        [DefaultValue(150.0f)]
        public float FocalLength
        {
            get
            {
                return focalLength;
            }
            set
            {
                focalLength = value;
            }
        }

        [DefaultValue(50)]
        public int OffsetY
        {
            get
            {
                return offsetY;
            }
            set
            {
                offsetY = value;
            }
        }


        [DefaultValue(true)]
        public bool UseScale
        {
            get
            {
                return useScale;
            }
            set
            {
                useScale = value;
            }
        }


        [DefaultValue(MoveDirection.Right)]
        public MoveDirection MoveDirection
        {
            get
            {
                return moveDirection;
            }
            set
            {
                moveDirection = value;
            }
        }


        public override void DoSizeChanged()
        {
            UpdateCoordinates();
            Rotate();
        }

        public virtual void UpdateCoordinates()
        {
            int Total = items.Count;
            if (Total > 0)
            {
                int TurnAngle = (int)(360 / Total);
                int Angle = 0;
                int reflection = GetReflectionDepth();
                int defSize = GetIconSize();

                for (int cnt = 0; cnt < Total; cnt++)
                {
                    items[cnt].Angle = Angle;
                    items[cnt].X = 0;
                    items[cnt].Y = 0;
                    items[cnt].Width = defSize;
                    items[cnt].Height = defSize + reflection;
                    items[cnt].ReflectionDepth = reflection;
                    Angle += TurnAngle;

                }
            }
        }

        public virtual void Rotate()
        {
            int Total = items.Count;
            if (Total == 0)
                return;

            if (focalLength < 0.02f) 
                focalLength = minFocalLength;


            int Xcenter = (Width - GetIconSize())/ 2;
            int Ycenter = (Height - GetIconSize()) / 2;
            int Zcenter = Ycenter / 2;


                for (int cnt = Total - 1; cnt > -1; cnt--)
                {
                    float angle = items[cnt].Angle;
                    float anglePi = (float)(angle * KarnaMath.PiDiv180);
                    float z = (float)(Math.Sin(anglePi) * Radius + Zcenter);
                    float scale = focalLength / (focalLength + z);

                    if (focalLength == minFocalLength) { 
                        items[cnt].X = Xcenter;
                        items[cnt].Y = Ycenter + offsetY; }
                    else  {
                        int X = (int)(Math.Cos(anglePi) * Radius);
                        items[cnt].X = (int)(X * scale + Xcenter);
                        items[cnt].Y = (int)(offsetY * scale + Ycenter - offsetY);
                    }

                    items[cnt].Order = (int)(angle - 90); 
                    if (items[cnt].Order > 180) items[cnt].Order = 360 - items[cnt].Order;
                
                    if (moveDirection == MoveDirection.Left)
                    {
                        angle -= speed; 
                        if (angle < 1) 
                            angle = 360;
                    }
                    else
                    {
                        angle += speed; 
                        if (angle > 359) 
                            angle = angle - 360;
                    }

                    items[cnt].Angle = angle;

                    if (useScale)
                    {
                        items[cnt].Scale =  (Math.Abs(items[cnt].Order) % 182) * 0.0027777f + 0.5f;
                    }
                    else
                    {
                        items[cnt].Scale = 1.0f;
                    }

                    if (UseAlpha)
                    {
                        if (focalLength > 0.999) 
                            items[cnt].Alpha =  (byte)Math.Min(100 * scale, 255);
                    }
                    else
                    {
                        items[cnt].Alpha = 255;
                    }
                }

                items.Sort(new ZOrderComparer());
            }


    }
}
