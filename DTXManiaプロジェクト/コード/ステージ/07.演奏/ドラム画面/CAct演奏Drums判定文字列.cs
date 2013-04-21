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
                        this.stレーンサイズ[i].x = sizeXW[9, 0] - 24;
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
                while (index < 12)
                {
                    if (!base.st状態[index].ct進行.b停止中)
                    {
                        //#if 旧判定アニメ
                        base.st状態[index].ct進行.t進行();
                        if (base.st状態[index].ct進行.b終了値に達した)
                        {
                            base.st状態[index].ct進行.t停止();
                        }
                        base.st状態[index].nRect = base.st状態[index].ct進行.n現在の値;
                    }
                    //#endif
                    index++;
                }
                        
                for (int j = 0; j < 12; j++)
                {
                    if (!base.st状態[j].ct進行.b停止中)
                    {
                        int num4 = base.st判定文字列[(int)base.st状態[j].judge].n画像番号;
                        int num5 = 0;
                        int num6 = 0;
                        if (j < 10)
                        {
                            num5 = this.stレーンサイズ[j].x;
                            num6 = CDTXMania.ConfigIni.bReverse.Drums ? ((CDTXMania.ConfigIni.判定文字表示位置.Drums == E判定文字表示位置.レーン上) ? (240 + (this.n文字の縦表示位置[j] * 0x20)) : 50) : ((CDTXMania.ConfigIni.判定文字表示位置.Drums == E判定文字表示位置.レーン上) ? (base.iP_A + (this.n文字の縦表示位置[j] * 0x20)) : base.iP_B);
                        }
                        else if (j == 11)
                        {
                            if (CDTXMania.ConfigIni.判定文字表示位置.Bass == E判定文字表示位置.表示OFF)
                            {
                                goto Label_09F4;
                            }
                            num5 = this.stレーンサイズ[j].x;
                            num6 = CDTXMania.ConfigIni.bReverse.Bass ? (((CDTXMania.ConfigIni.判定文字表示位置.Bass == E判定文字表示位置.レーン上) ? 240 : 100) + (this.n文字の縦表示位置[j] * 0x20)) : (((CDTXMania.ConfigIni.判定文字表示位置.Bass == E判定文字表示位置.レーン上) ? 180 : 300) + (this.n文字の縦表示位置[j] * 0x20));
                        }
                        else if (j == 10)
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
                                num5 = this.stレーンサイズ[j].x;
                                num6 = CDTXMania.ConfigIni.bReverse.Guitar ? (((CDTXMania.ConfigIni.判定文字表示位置.Guitar == E判定文字表示位置.レーン上) ? 240 : 100) + (this.n文字の縦表示位置[j] * 0x20)) : (((CDTXMania.ConfigIni.判定文字表示位置.Guitar == E判定文字表示位置.レーン上) ? 180 : 300) + (this.n文字の縦表示位置[j] * 0x20));
                            }
                        }

                        int nRectX = CDTXMania.ConfigIni.nJudgeWidgh;
                        int nRectY = CDTXMania.ConfigIni.nJudgeHeight;

                        int xc = (num5 + base.st状態[j].n相対X座標) + (this.stレーンサイズ[j].w / 2);
                        int x = (xc - ((int)((110f * base.st状態[j].fX方向拡大率) * ((j < 10) ? 1.0 : 0.7)))) - (( nRectX - 225 ) / 2) ;
                        int y = ((num6 + base.st状態[j].n相対Y座標) - ((int)(((140f * base.st状態[j].fY方向拡大率) * ((j < 10) ? 1.0 : 0.7)) / 2.0))) - (( nRectY - 135 ) / 2);


                        if (base.tx判定文字列[num4] != null)
                        {
                            if (base.st状態[j].judge == E判定.Perfect)
                            {
                                base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                            }
                            if (base.st状態[j].judge == E判定.Great)
                            {
                                base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 1, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                            }
                            if (base.st状態[j].judge == E判定.Good)
                            {
                                base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 2, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                            }
                            if (base.st状態[j].judge == E判定.Poor)
                            {
                                base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 3, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                            }
                            if (base.st状態[j].judge == E判定.Miss)
                            {
                                base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 4, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                            }
                            if (base.st状態[j].judge == E判定.Auto)
                            {
                                base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 5, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                            }


                            if (base.nShowLagType == (int)EShowLagType.ON ||
                                 ((base.nShowLagType == (int)EShowLagType.GREAT_POOR) && (base.st状態[j].judge != E判定.Perfect)))
                            {
                                if (base.st状態[j].judge != E判定.Auto && base.txlag数値 != null)		// #25370 2011.2.1 yyagi
                                {
                                    bool minus = false;
                                    int offsetX = 0;
                                    string strDispLag = base.st状態[j].nLag.ToString();
                                    if (st状態[j].nLag < 0)
                                    {
                                        minus = true;
                                    }
                                    x = xc - strDispLag.Length * 15 / 2;
                                    for (int i = 0; i < strDispLag.Length; i++)
                                    {
                                        int p = (strDispLag[i] == '-') ? 11 : (int)(strDispLag[i] - '0');	//int.Parse(strDispLag[i]);
                                        p += minus ? 0 : 12;		// change color if it is minus value
                                        base.txlag数値.t2D描画(CDTXMania.app.Device, x + offsetX, y + 34, base.stLag数値[p].rc);
                                        offsetX += 15;
                                    }
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
