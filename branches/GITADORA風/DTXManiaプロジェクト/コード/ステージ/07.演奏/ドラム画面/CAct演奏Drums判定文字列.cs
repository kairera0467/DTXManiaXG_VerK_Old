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
            base.b活性化してない = true;
        }
		

		// CActivity 実装（共通クラスからの差分のみ）

        public override int On進行描画()
        {
            if (!base.b活性化してない)
            {
                int index = 0;
                if (CDTXMania.ConfigIni.nJudgeFrames > 1)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        if (!base.st状態[i].ct進行.b停止中)
                        {
                            base.st状態[i].ct進行.t進行();
                            if (base.st状態[i].ct進行.b終了値に達した)
                            {
                                base.st状態[i].ct進行.t停止();
                            }
                            base.st状態[i].nRect = base.st状態[i].ct進行.n現在の値;
                        }
                        index++;
                    }
                }
                else
                {
                    for (int i = 0; i < 12; i++)
                    {
                        if (!base.st状態[i].ct進行.b停止中)
                        {
                            base.st状態[i].ct進行.t進行();
                            if (base.st状態[i].ct進行.b終了値に達した)
                            {
                                base.st状態[i].ct進行.t停止();
                            }
                            int num2 = base.st状態[i].ct進行.n現在の値;
                            if ((base.st状態[i].judge != E判定.Miss) && (base.st状態[i].judge != E判定.Bad))
                            {
                                if (num2 < 50)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f + (1f * (1f - (((float)num2) / 50f)));
                                    base.st状態[i].fY方向拡大率 = ((float)num2) / 50f;
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = 0;
                                    base.st状態[i].n透明度 = 0xff;
                                }
                                else if (num2 < 130)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = ((num2 % 6) == 0) ? (CDTXMania.Random.Next(6) - 3) : base.st状態[i].n相対Y座標;
                                    base.st状態[i].n透明度 = 0xff;
                                }
                                else if (num2 >= 240)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f - ((1f * (num2 - 240)) / 60f);
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = 0;
                                    base.st状態[i].n透明度 = 0xff;
                                }
                                else
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = 0;
                                    base.st状態[i].n透明度 = 0xff;
                                }
                            }
                            else if (num2 < 50)
                            {
                                base.st状態[i].fX方向拡大率 = 1f;
                                base.st状態[i].fY方向拡大率 = ((float)num2) / 50f;
                                base.st状態[i].n相対X座標 = 0;
                                base.st状態[i].n相対Y座標 = 0;
                                base.st状態[i].n透明度 = 0xff;
                            }
                            else if (num2 >= 200)
                            {
                                base.st状態[i].fX方向拡大率 = 1f - (((float)(num2 - 200)) / 100f);
                                base.st状態[i].fY方向拡大率 = 1f - (((float)(num2 - 200)) / 100f);
                                base.st状態[i].n相対X座標 = 0;
                                base.st状態[i].n相対Y座標 = 0;
                                base.st状態[i].n透明度 = 0xff;
                            }
                            else
                            {
                                base.st状態[i].fX方向拡大率 = 1f;
                                base.st状態[i].fY方向拡大率 = 1f;
                                base.st状態[i].n相対X座標 = 0;
                                base.st状態[i].n相対Y座標 = 0;
                                base.st状態[i].n透明度 = 0xff;
                            }
                        }
                    }
                }
                        
                for (int j = 0; j < 12; j++)
                {
                    if (!base.st状態[j].ct進行.b停止中)
                    {
                        int num4 = CDTXMania.ConfigIni.nJudgeFrames > 1 ? 0 : base.st判定文字列[(int)base.st状態[j].judge].n画像番号;
                        int num5 = 0;
                        int num6 = 0;
                        if (j < 10)
                        {
                            num5 = base.stレーンサイズ[j].x;
                            num6 = CDTXMania.ConfigIni.bReverse.Drums ? ((CDTXMania.ConfigIni.判定文字表示位置.Drums == Eタイプ.A) ? (240 + (this.n文字の縦表示位置[j] * 0x20)) : 50) : ((CDTXMania.ConfigIni.判定文字表示位置.Drums == Eタイプ.A) ? (base.iP_A + (this.n文字の縦表示位置[j] * 0x20)) : base.iP_B);
                        }
                        else if (j == 11)
                        {
                            if (CDTXMania.ConfigIni.判定文字表示位置.Bass == Eタイプ.C)
                            {
                                continue;
                            }
                            num5 = base.stレーンサイズ[j].x;
                            num6 = CDTXMania.ConfigIni.bReverse.Bass ? (((CDTXMania.ConfigIni.判定文字表示位置.Bass == Eタイプ.A) ? 240 : 100) + (this.n文字の縦表示位置[j] * 0x20)) : (((CDTXMania.ConfigIni.判定文字表示位置.Bass == Eタイプ.A) ? 180 : 300) + (this.n文字の縦表示位置[j] * 0x20));
                        }
                        else if (j == 10)
                        {
                            if (CDTXMania.ConfigIni.判定文字表示位置.Guitar == Eタイプ.C)
                            {
                                continue;
                            }
                            if (!(CDTXMania.DTX.bチップがある.Bass || (CDTXMania.ConfigIni.判定文字表示位置.Guitar != Eタイプ.B)))
                            {
                                num5 = 0x198;
                                num6 = 0x93;
                            }
                            else
                            {
                                num5 = this.stレーンサイズ[j].x;
                                num6 = CDTXMania.ConfigIni.bReverse.Guitar ? (((CDTXMania.ConfigIni.判定文字表示位置.Guitar == Eタイプ.A) ? 240 : 100) + (this.n文字の縦表示位置[j] * 0x20)) : (((CDTXMania.ConfigIni.判定文字表示位置.Guitar == Eタイプ.A) ? 180 : 300) + (this.n文字の縦表示位置[j] * 0x20));
                            }
                        }

                        int nRectX = CDTXMania.ConfigIni.nJudgeWidgh;
                        int nRectY = CDTXMania.ConfigIni.nJudgeHeight;

                        int xc = (num5 + base.st状態[j].n相対X座標) + (this.stレーンサイズ[j].w / 2);
                        int x = (xc - ((int)((110f * base.st状態[j].fX方向拡大率) * ((j < 10) ? 1.0 : 0.7)))) - (( nRectX - 225 ) / 2) ;
                        int y = ((num6 + base.st状態[j].n相対Y座標) - ((int)(((140f * base.st状態[j].fY方向拡大率) * ((j < 10) ? 1.0 : 0.7)) / 2.0))) - (( nRectY - 135 ) / 2);

                        //if (base.tx判定文字列[num4] != null)
                        {
                            if (CDTXMania.ConfigIni.nJudgeFrames > 1 && CDTXMania.stage演奏ドラム画面.tx判定画像anime != null)
                            {
                                if (base.st状態[j].judge == E判定.Perfect)
                                {
                                    //base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                }
                                if (base.st状態[j].judge == E判定.Great)
                                {
                                    //base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 1, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 1, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                }
                                if (base.st状態[j].judge == E判定.Good)
                                {
                                    //base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 2, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 2, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                }
                                if (base.st状態[j].judge == E判定.Poor)
                                {
                                    //base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 3, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 3, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                }
                                if (base.st状態[j].judge == E判定.Miss)
                                {
                                    //base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 4, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 4, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                }
                                if (base.st状態[j].judge == E判定.Auto)
                                {
                                    //base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 5, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 5, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                }
                            }
                            else if (base.tx判定文字列[num4] != null)
                            {
                                x = xc - ((int)((64f * base.st状態[j].fX方向拡大率) * ((j < 10) ? 1.0 : 0.7)));
                                y = (num6 + base.st状態[j].n相対Y座標) - ((int)(((43f * base.st状態[j].fY方向拡大率) * ((j < 10) ? 1.0 : 0.7)) / 2.0));

                                base.tx判定文字列[num4].n透明度 = base.st状態[j].n透明度;
                                base.tx判定文字列[num4].vc拡大縮小倍率 = new Vector3((float)(base.st状態[j].fX方向拡大率 * ((j < 10) ? 1.0 : 0.7)), (float)(base.st状態[j].fY方向拡大率 * ((j < 10) ? 1.0 : 0.7)), 1f);
                                base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, base.st判定文字列[(int)base.st状態[j].judge].rc);
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
                    }
                }
            }
            return 0;
        }

 

 

		

		// その他

		#region [ private ]
		//-----------------
        private readonly int[] n文字の縦表示位置 = new int[] { -1, 1, 1, 2, 0, 0, 1, -1, 2, 1, 2, -1, -1, 0, 0 };

		//-----------------
		#endregion
	}
}
