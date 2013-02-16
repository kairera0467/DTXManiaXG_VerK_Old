using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Runtime.InteropServices;
using SlimDX;
using SlimDX.Direct3D9;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Drums判定文字列 : CAct演奏判定文字列共通
	{
		// コンストラクタ

        public CAct演奏Drums判定文字列()
        {
            this.stレーンサイズ = new STレーンサイズ[12];
            STレーンサイズ stレーンサイズ = new STレーンサイズ();
            //                                LC          HH          SD            BD          HT           LT           FT            CY          LP             RD
            int[,] sizeXW = new int[,] { { 290, 80 }, { 367, 46 }, { 470, 54 }, { 582, 60 }, { 528, 46 }, { 645, 46 }, { 694, 46 }, { 748, 64 }, { 419, 46 }, { 815, 80 }, { 815, 80 }, { 815, 80 }, };
            int[,] sizeXW_B = new int[,] { { 290, 80 }, { 367, 46 }, { 419, 54 }, { 534, 60 }, { 590, 46 }, { 645, 46 }, { 694, 46 }, { 748, 64 }, { 478, 46 }, { 815, 64 }, { 815, 80 }, { 507, 80 }, };
            int[,] sizeXW_C = new int[,] { { 290, 80 }, { 367, 46 }, { 470, 54 }, { 534, 60 }, { 590, 46 }, { 645, 46 }, { 694, 46 }, { 748, 64 }, { 419, 46 }, { 815, 64 }, { 815, 80 }, { 507, 80 }, };
            int[,] sizeXW_D = new int[,] { { 290, 80 }, { 367, 46 }, { 470, 54 }, { 534, 60 }, { 590, 46 }, { 645, 46 }, { 694, 46 }, { 748, 64 }, { 419, 46 }, { 815, 64 }, { 815, 80 }, { 507, 80 }, };

            for (int i = 0; i < 12; i++)
            {
                this.stレーンサイズ[i] = new STレーンサイズ();

                {
                    this.stレーンサイズ[i] = default(CAct演奏Drums判定文字列.STレーンサイズ);
                    switch (CDTXMania.ConfigIni.eLaneType.Drums)
                    {
                        case Eタイプ.A:
                        this.stレーンサイズ[i].x = sizeXW[i, 0];
                        this.stレーンサイズ[i].w = sizeXW[i, 1];
                        goto IL_19F;
                        case Eタイプ.B:
                        this.stレーンサイズ[i].x = sizeXW_B[i, 0];
                        this.stレーンサイズ[i].w = sizeXW_B[i, 1];
                        goto IL_19F;
                        case Eタイプ.C:
                        this.stレーンサイズ[i].x = sizeXW_C[i, 0];
                        this.stレーンサイズ[i].w = sizeXW_C[i, 1];
                        goto IL_19F;
                        case Eタイプ.D:
                        this.stレーンサイズ[i].x = sizeXW_D[i, 0];
                        this.stレーンサイズ[i].w = sizeXW_D[i, 1];
                        goto IL_19F;
                    }
                IL_19F:
                    if (i == 7 && CDTXMania.ConfigIni.eRDPosition == ERDPosition.RDRC)
                    {
                        this.stレーンサイズ[i].x = sizeXW[9, 0];
                    }
                    if (i == 9 && CDTXMania.ConfigIni.eRDPosition == ERDPosition.RDRC)
                    {
                        this.stレーンサイズ[i].x = sizeXW[7, 0];
                    }
                }
            }




            base.b活性化してない = true;
        }
		

		// CActivity 実装（共通クラスからの差分のみ）

        public override int On進行描画()
        {
            if (!base.b活性化してない)
            {
                int index = 0;
                int numYPER = 0;
                int numYGRE = 0;
                int numYGO = 0;
                int numYPO = 0;
                int numYMI = 0;
                while (index < 12)
                {
                    if (!base.st状態[index].ct進行.b停止中)
                    {
                        
                        base.st状態[index].ct進行.t進行();
                        if (base.st状態[index].ct進行.b終了値に達した)
                        {
                            base.st状態[index].ct進行.t停止();
                        }
                        int num2 = base.st状態[index].ct進行.n現在の値;
                        

                    
                   
                        if (base.st状態[index].judge == E判定.Perfect || base.st状態[index].judge == E判定.Auto)
                        {
                            base.st状態[index].ctPERFECT.t進行();
                            if (base.st状態[index].ctPERFECT.b終了値に達した)
                            {
                                base.st状態[index].ctPERFECT.t停止();
                            }
                        }
                        numYPER = base.st状態[index].ctPERFECT.n現在の値;
                    

                        if (base.st状態[index].judge == E判定.Great)
                        {
                            base.st状態[index].ctGREAT.t進行();
                            if (base.st状態[index].ctGREAT.b終了値に達した)
                            {
                                base.st状態[index].ctGREAT.t停止();
                            }
                        }
                        numYGRE = base.st状態[index].ctGREAT.n現在の値;

                        if (base.st状態[index].judge == E判定.Good)
                        {
                            base.st状態[index].ctGOOD.t進行();
                            if (base.st状態[index].ctGOOD.b終了値に達した)
                            {
                                base.st状態[index].ctGOOD.t停止();
                            }
                        }
                        numYGO = base.st状態[index].ctGOOD.n現在の値;

                        if (base.st状態[index].judge == E判定.Poor)
                        {
                            base.st状態[index].ctPOOR.t進行();
                            if (base.st状態[index].ctPOOR.b終了値に達した)
                            {
                                base.st状態[index].ctPOOR.t停止();
                            }
                        }
                        numYPO = base.st状態[index].ctPOOR.n現在の値;

                        if (base.st状態[index].judge == E判定.Miss)
                        {
                            base.st状態[index].ctMISS.t進行();
                            if (base.st状態[index].ctMISS.b終了値に達した)
                            {
                                base.st状態[index].ctMISS.t停止();
                            }
                        }
                        numYMI = base.st状態[index].ctMISS.n現在の値;
                        

                        #if 旧判定アニメ
                        
                        if ((base.st状態[index].judge != E判定.Miss) && (base.st状態[index].judge != E判定.Bad))
                        {
                            if (num2 < 50)
                            {
                                base.st状態[index].fX方向拡大率 = 1f + (1f * (1f - (((float)num2) / 50f)));
                                base.st状態[index].fY方向拡大率 = ((float)num2) / 50f;
                                base.st状態[index].n相対X座標 = 0;
                                base.st状態[index].n相対Y座標 = 0;
                                base.st状態[index].n透明度 = 0xff;
                            }
                            else if (num2 < 130)
                            {
                                base.st状態[index].fX方向拡大率 = 1f;
                                base.st状態[index].fY方向拡大率 = 1f;
                                base.st状態[index].n相対X座標 = 0;
                                base.st状態[index].n相対Y座標 = ((num2 % 6) == 0) ? (CDTXMania.Random.Next(6) - 3) : base.st状態[index].n相対Y座標;
                                base.st状態[index].n透明度 = 0xff;
                            }
                            else if (num2 >= 240)
                            {
                                base.st状態[index].fX方向拡大率 = 1f;
                                base.st状態[index].fY方向拡大率 = 1f - ((1f * (num2 - 240)) / 60f);
                                base.st状態[index].n相対X座標 = 0;
                                base.st状態[index].n相対Y座標 = 0;
                                base.st状態[index].n透明度 = 0xff;
                            }
                            else
                            {
                                base.st状態[index].fX方向拡大率 = 1f;
                                base.st状態[index].fY方向拡大率 = 1f;
                                base.st状態[index].n相対X座標 = 0;
                                base.st状態[index].n相対Y座標 = 0;
                                base.st状態[index].n透明度 = 0xff;
                            }
                        }
                        else if (num2 < 50)
                        {
                            base.st状態[index].fX方向拡大率 = 1f;
                            base.st状態[index].fY方向拡大率 = ((float)num2) / 50f;
                            base.st状態[index].n相対X座標 = 0;
                            base.st状態[index].n相対Y座標 = 0;
                            base.st状態[index].n透明度 = 0xff;
                        }
                        else if (num2 >= 200)
                        {
                            base.st状態[index].fX方向拡大率 = 1f - (((float)(num2 - 200)) / 100f);
                            base.st状態[index].fY方向拡大率 = 1f - (((float)(num2 - 200)) / 100f);
                            base.st状態[index].n相対X座標 = 0;
                            base.st状態[index].n相対Y座標 = 0;
                            base.st状態[index].n透明度 = 0xff;
                        }
                        else
                        {
                            base.st状態[index].fX方向拡大率 = 1f;
                            base.st状態[index].fY方向拡大率 = 1f;
                            base.st状態[index].n相対X座標 = 0;
                            base.st状態[index].n相対Y座標 = 0;
                            base.st状態[index].n透明度 = 0xff;
                        }
                        #endif
                    }
                    
                    index++;
                }
                        
                for (int i = 0; i < 12; i++)
                {
                    if (!base.st状態[i].ct進行.b停止中)
                    {
                        int num4 = base.st判定文字列[(int)base.st状態[i].judge].n画像番号;
                        int num5 = 0;
                        int num6 = 0;
                        if (i < 10)
                        {
                            num5 = this.stレーンサイズ[i].x;
                            num6 = CDTXMania.ConfigIni.bReverse.Drums ? ((CDTXMania.ConfigIni.判定文字表示位置.Drums == E判定文字表示位置.レーン上) ? (240 + (this.n文字の縦表示位置[i] * 0x20)) : 50) : ((CDTXMania.ConfigIni.判定文字表示位置.Drums == E判定文字表示位置.レーン上) ? (base.iP_A + (this.n文字の縦表示位置[i] * 0x20)) : base.iP_B);
                        }
                        else if (i == 11)
                        {
                            if (CDTXMania.ConfigIni.判定文字表示位置.Bass == E判定文字表示位置.表示OFF)
                            {
                                goto Label_09F4;
                            }
                            num5 = this.stレーンサイズ[i].x;
                            num6 = CDTXMania.ConfigIni.bReverse.Bass ? (((CDTXMania.ConfigIni.判定文字表示位置.Bass == E判定文字表示位置.レーン上) ? 240 : 100) + (this.n文字の縦表示位置[i] * 0x20)) : (((CDTXMania.ConfigIni.判定文字表示位置.Bass == E判定文字表示位置.レーン上) ? 180 : 300) + (this.n文字の縦表示位置[i] * 0x20));
                        }
                        else if (i == 10)
                        {
                            if (CDTXMania.ConfigIni.判定文字表示位置.Guitar == E判定文字表示位置.表示OFF)
                            {
                                goto Label_09F4;
                            }
                            if (!(CDTXMania.DTX.bチップがある.Bass || (CDTXMania.ConfigIni.判定文字表示位置.Guitar != E判定文字表示位置.判定ライン上または横)))
                            {
                                num5 = 0x198;
                                num6 = 0x93;
                            }
                            else
                            {
                                num5 = this.stレーンサイズ[i].x;
                                num6 = CDTXMania.ConfigIni.bReverse.Guitar ? (((CDTXMania.ConfigIni.判定文字表示位置.Guitar == E判定文字表示位置.レーン上) ? 240 : 100) + (this.n文字の縦表示位置[i] * 0x20)) : (((CDTXMania.ConfigIni.判定文字表示位置.Guitar == E判定文字表示位置.レーン上) ? 180 : 300) + (this.n文字の縦表示位置[i] * 0x20));
                            }
                        }
                        int num7 = (num5 + base.st状態[i].n相対X座標) + (this.stレーンサイズ[i].w / 2);
                        int x = num7 - ((int)((64f * base.st状態[i].fX方向拡大率) * ((i < 10) ? 1.0 : 0.7))) - 48;
                        int y = (num6 + base.st状態[i].n相対Y座標) - ((int)(((43f * base.st状態[i].fY方向拡大率) * ((i < 10) ? 1.0 : 0.7)) / 2.0));

                        if (base.tx判定文字列[num4] != null)
                        {
                            //base.tx判定文字列[num4].n透明度 = base.st状態[i].n透明度;
                            //base.tx判定文字列[num4].vc拡大縮小倍率 = new Vector3((float)(base.st状態[i].fX方向拡大率 * ((i < 10) ? 1.0 : 0.7)), (float)(base.st状態[i].fY方向拡大率 * ((i < 10) ? 1.0 : 0.7)), 1f);
                            switch (base.st状態[num4].judge)
                            {
                                case E判定.Perfect:
                                    base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, 0 + (numYPER * 135), 225, 135));
                                    break;
                                case E判定.Great:
                                    base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(225, (numYGRE * 135), 225, 135));
                                    break;
                                case E判定.Good:
                                    base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(450, (numYGO * 105), 225, 105));
                                    break;
                                case E判定.Poor:
                                    base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, (numYPO * 90), 225, 90));
                                    break;
                                case E判定.Miss:
                                    base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(226, (numYMI * 90), 225, 90));
                                    break;
                                case E判定.Auto:
                                    base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, (numYPER * 135), 225, 135));
                                    break;
                                default:
                                    break;
                            }

                            if ((((base.st状態[i].judge != E判定.Auto) && (base.st状態[i].judge != E判定.Perfect)) && (base.st状態[i].judge != E判定.Miss)) && (base.txlag数値 != null))
                            {
                                int num10 = 0;
                                int num11 = 0;
                                string str = (base.st状態[i].nLag * -1).ToString();
                                y += (CDTXMania.ConfigIni.判定文字表示位置.Drums == E判定文字表示位置.レーン上) ? -20 : 40;
                                if (str[0] == '-')
                                {
                                    num10 = 12;
                                }
                                else
                                {
                                    num10 = 0;
                                }
                                x = num7 - ((str.Length * 15) / 2);
                                for (index = 0; index < str.Length; index++)
                                {
                                    int num13 = (str[index] == '-') ? 11 : (str[index] - '0');
                                    num13 += num10;
                                    base.txlag数値.t2D描画(CDTXMania.app.Device, x + num11, y, base.stLag数値[num13].rc);
                                    num11 += 15;
                                }
                            }

                        }
                    Label_09F4: ;
                    }
                }
            }
            return 0;
        }

 

 

		

		// その他

		#region [ private ]
		//-----------------
		[StructLayout( LayoutKind.Sequential )]
		private struct STレーンサイズ
		{
			public int x;
			public int w;
		}

        private readonly int[] n文字の縦表示位置 = new int[] { -1, 1, 1, 2, 0, 0, 1, -1, 2, 1, 2, -1, -1, 0, 0 };
		private STレーンサイズ[] stレーンサイズ;

		//-----------------
		#endregion
	}
}
