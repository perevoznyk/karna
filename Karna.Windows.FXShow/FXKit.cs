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

namespace Karna.Windows.FXShow
{

    /// <summary>
    /// Represents the method that will handle transition drawing
    /// </summary>
    /// <param name="D">Handle of the native Windows bitmap for the surface</param> 
    /// <param name="H">Surface height</param>
    /// <param name="P">Handle of the native Windows bitmap for the texture</param>
    /// <param name="W">Surface width</param>
    /// <param name="Progress">Transition progress (0..100)</param>
    /// <param name="X">X coordinate of the transition</param>
    /// <param name="Y">Y coordinate of the transition</param>
    public delegate void FXEffect(IntPtr D, IntPtr P, int W, int H, int X, int Y, int Progress);

    /// <summary>
    /// Dynamically selects transition method
    /// </summary>
    public static class FXKit
    {
        /// <summary>
        /// Selects the effect number.
        /// </summary>
        /// <param name="number">The effect number.</param>
        /// <returns>Delegate that implements selected transition</returns>
        public static FXEffect SelectEffectNumber(int number)
        {

            switch (number)
            {
                case 001: return NativeMethods.EffectW001;
                case 002: return NativeMethods.EffectW002;
                case 003: return NativeMethods.EffectW003;
                case 004: return NativeMethods.EffectW004;
                case 005: return NativeMethods.EffectW005;
                case 006: return NativeMethods.EffectW006;
                case 007: return NativeMethods.EffectW007;
                case 008: return NativeMethods.EffectW008;
                case 009: return NativeMethods.EffectW009;
                case 010: return NativeMethods.EffectW010;
                case 011: return NativeMethods.EffectW011;
                case 012: return NativeMethods.EffectW012;
                case 013: return NativeMethods.EffectW013;
                case 014: return NativeMethods.EffectW014;
                case 015: return NativeMethods.EffectW015;
                case 016: return NativeMethods.EffectW016;
                case 017: return NativeMethods.EffectW017;
                case 018: return NativeMethods.EffectW018;
                case 019: return NativeMethods.EffectW019;
                case 020: return NativeMethods.EffectW020;
                case 021: return NativeMethods.EffectW021;
                case 022: return NativeMethods.EffectW022;
                case 023: return NativeMethods.EffectW023;
                case 024: return NativeMethods.EffectW024;
                case 025: return NativeMethods.EffectW025;
                case 026: return NativeMethods.EffectW026;
                case 027: return NativeMethods.EffectW027;
                case 028: return NativeMethods.EffectW028;
                case 029: return NativeMethods.EffectW029;
                case 030: return NativeMethods.EffectW030;
                case 031: return NativeMethods.EffectW031;
                case 032: return NativeMethods.EffectW032;
                case 033: return NativeMethods.EffectW033;
                case 034: return NativeMethods.EffectW034;
                case 035: return NativeMethods.EffectW035;
                case 036: return NativeMethods.EffectW036;
                case 037: return NativeMethods.EffectW037;
                case 038: return NativeMethods.EffectW038;
                case 039: return NativeMethods.EffectW039;
                case 040: return NativeMethods.EffectW040;
                case 041: return NativeMethods.EffectW041;
                case 042: return NativeMethods.EffectW042;
                case 043: return NativeMethods.EffectW043;
                case 044: return NativeMethods.EffectW044;
                case 045: return NativeMethods.EffectW045;
                case 046: return NativeMethods.EffectW046;
                case 047: return NativeMethods.EffectW047;
                case 048: return NativeMethods.EffectW048;
                case 049: return NativeMethods.EffectW049;
                case 050: return NativeMethods.EffectW050;
                case 051: return NativeMethods.EffectW051;
                case 052: return NativeMethods.EffectW052;
                case 053: return NativeMethods.EffectW053;
                case 054: return NativeMethods.EffectW054;
                case 055: return NativeMethods.EffectW055;
                case 056: return NativeMethods.EffectW056;
                case 057: return NativeMethods.EffectW057;
                case 058: return NativeMethods.EffectW058;
                case 059: return NativeMethods.EffectW059;
                case 060: return NativeMethods.EffectW060;
                case 061: return NativeMethods.EffectW061;
                case 062: return NativeMethods.EffectW062;
                case 063: return NativeMethods.EffectW063;
                case 064: return NativeMethods.EffectW064;
                case 065: return NativeMethods.EffectW065;
                case 066: return NativeMethods.EffectW066;
                case 067: return NativeMethods.EffectW067;
                case 068: return NativeMethods.EffectW068;
                case 069: return NativeMethods.EffectW069;
                case 070: return NativeMethods.EffectW070;
                case 071: return NativeMethods.EffectW071;
                case 072: return NativeMethods.EffectW072;
                case 073: return NativeMethods.EffectW073;
                case 074: return NativeMethods.EffectW074;
                case 075: return NativeMethods.EffectW075;
                case 076: return NativeMethods.EffectW076;
                case 077: return NativeMethods.EffectW077;
                case 078: return NativeMethods.EffectW078;
                case 079: return NativeMethods.EffectW079;
                case 080: return NativeMethods.EffectW080;
                case 081: return NativeMethods.EffectW081;
                case 082: return NativeMethods.EffectW082;
                case 083: return NativeMethods.EffectW083;
                case 084: return NativeMethods.EffectW084;
                case 085: return NativeMethods.EffectW085;
                case 086: return NativeMethods.EffectW086;
                case 087: return NativeMethods.EffectW087;
                case 088: return NativeMethods.EffectW088;
                case 089: return NativeMethods.EffectW089;
                case 090: return NativeMethods.EffectW090;
                case 091: return NativeMethods.EffectW091;
                case 092: return NativeMethods.EffectW092;
                case 093: return NativeMethods.EffectW093;
                case 094: return NativeMethods.EffectW094;
                case 095: return NativeMethods.EffectW095;
                case 096: return NativeMethods.EffectW096;
                case 097: return NativeMethods.EffectW097;
                case 098: return NativeMethods.EffectW098;
                case 099: return NativeMethods.EffectW099;
                case 100: return NativeMethods.EffectW100;
                case 101: return NativeMethods.EffectW101;
                case 102: return NativeMethods.EffectW102;
                case 103: return NativeMethods.EffectW103;
                case 104: return NativeMethods.EffectW104;
                case 105: return NativeMethods.EffectW105;
                case 106: return NativeMethods.EffectW106;
                case 107: return NativeMethods.EffectW107;
                case 108: return NativeMethods.EffectW108;
                case 109: return NativeMethods.EffectW109;
                case 110: return NativeMethods.EffectW110;
                case 111: return NativeMethods.EffectW111;
                case 112: return NativeMethods.EffectW112;
                case 113: return NativeMethods.EffectW113;
                case 114: return NativeMethods.EffectW114;
                case 115: return NativeMethods.EffectW115;
                case 116: return NativeMethods.EffectW116;
                case 117: return NativeMethods.EffectW117;
                case 118: return NativeMethods.EffectW118;
                case 119: return NativeMethods.EffectW119;
                case 120: return NativeMethods.EffectW120;
                case 121: return NativeMethods.EffectW121;
                case 122: return NativeMethods.EffectW122;
                case 123: return NativeMethods.EffectW123;
                case 124: return NativeMethods.EffectW124;
                case 125: return NativeMethods.EffectW125;
                case 126: return NativeMethods.EffectW126;
                case 127: return NativeMethods.EffectW127;
                case 128: return NativeMethods.EffectW128;
                case 129: return NativeMethods.EffectW129;
                case 130: return NativeMethods.EffectW130;
                case 131: return NativeMethods.EffectW131;
                case 132: return NativeMethods.EffectW132;
                case 133: return NativeMethods.EffectW133;
                case 134: return NativeMethods.EffectW134;
                case 135: return NativeMethods.EffectW135;
                case 136: return NativeMethods.EffectW136;
                case 137: return NativeMethods.EffectW137;
                case 138: return NativeMethods.EffectW138;
                case 139: return NativeMethods.EffectW139;
                case 140: return NativeMethods.EffectW140;
                case 141: return NativeMethods.EffectW141;
                case 142: return NativeMethods.EffectW142;
                case 143: return NativeMethods.EffectW143;
                case 144: return NativeMethods.EffectW144;
                case 145: return NativeMethods.EffectW145;
                case 146: return NativeMethods.EffectW146;
                case 147: return NativeMethods.EffectW147;
                case 148: return NativeMethods.EffectW148;
                case 149: return NativeMethods.EffectW149;
                case 150: return NativeMethods.EffectW150;
                case 151: return NativeMethods.EffectW151;
                case 152: return NativeMethods.EffectW152;
                case 153: return NativeMethods.EffectW153;
                case 154: return NativeMethods.EffectW154;
                case 155: return NativeMethods.EffectW155;
                case 156: return NativeMethods.EffectW156;
                case 157: return NativeMethods.EffectW157;
                case 158: return NativeMethods.EffectW158;
                case 159: return NativeMethods.EffectW159;
                case 160: return NativeMethods.EffectW160;
                case 161: return NativeMethods.EffectW161;
                case 162: return NativeMethods.EffectW162;
                case 163: return NativeMethods.EffectW163;
                case 164: return NativeMethods.EffectW164;
                case 165: return NativeMethods.EffectW165;
                case 166: return NativeMethods.EffectW166;
                case 167: return NativeMethods.EffectW167;
                case 168: return NativeMethods.EffectW168;
                case 169: return NativeMethods.EffectW169;
                case 170: return NativeMethods.EffectW170;
                case 171: return NativeMethods.EffectW171;
                case 172: return NativeMethods.EffectW172;
                default: return NativeMethods.EffectW001;
            }
        }
    }



}
