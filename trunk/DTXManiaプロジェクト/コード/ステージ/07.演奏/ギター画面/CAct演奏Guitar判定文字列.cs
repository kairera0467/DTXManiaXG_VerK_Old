using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Runtime.InteropServices;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Guitar判定文字列 : CAct演奏判定文字列共通
	{
		// コンストラクタ

		public CAct演奏Guitar判定文字列()
		{
			this.stレーンサイズ = new STレーンサイズ[ 12 ];
			STレーンサイズ stレーンサイズ = new STレーンサイズ();
            int[,] sizeXW = new int[,] 	{{30, 36},{71, 30},
		
		{
			135,
			30
		},
		
		{
			202,
			30
		},
		
		{
			167,
			30
		},
		
		{
			237,
			30
		},
		
		{
			269,
			30
		},
		
		{
			333,
			36
		},
		
		{
			103,
			30
		},
		
		{
			301,
			30
		},
		
		{
			103,
			30
		},
		
		{
			0,
			0
		},
		
		{
			0,
			0
		},
		
		{
			26,
			111
		},
		
		{
			480,
			111
		}
	};
			for ( int i = 0; i < 12; i++ )
			{
				this.stレーンサイズ[ i ] = new STレーンサイズ();
				this.stレーンサイズ[ i ].x = sizeXW[ i, 0 ];
				this.stレーンサイズ[ i ].w = sizeXW[ i, 1 ];
			}
			base.b活性化してない = true; 
		}


		// CActivity 実装（共通クラスからの差分のみ）

		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
                int n = 0;
                while (n < 12)
                {
                    if (!base.st状態[n].ct進行.b停止中)
                    {
                        base.st状態[n].ct進行.t進行();
                        if (base.st状態[n].ct進行.b終了値に達した)
                        {
                            base.st状態[n].ct進行.t停止();
                        }
                        base.st状態[n].nRect = base.st状態[n].ct進行.n現在の値;
                    }
                    n++;
				}
				for( int j = 0; j < 12; j++ )
				{
					if( !base.st状態[ j ].ct進行.b停止中 )
					{
						int index = base.st判定文字列[ (int) base.st状態[ j ].judge ].n画像番号;
						int num5 = 0;
						int num6 = 0;
						if( j >= 8 )
						{
							if( j == 11 )
							{
								if( ( (E判定文字表示位置) CDTXMania.ConfigIni.判定文字表示位置.Bass ) == E判定文字表示位置.表示OFF )
								{
									// goto Label_06B7;
									continue;
								}
								num5 = ( ( (E判定文字表示位置) CDTXMania.ConfigIni.判定文字表示位置.Bass ) == E判定文字表示位置.レーン上 ) ? 1000 : 175;
								num6 = CDTXMania.ConfigIni.bReverse.Bass ? 450 : 300;
							}
							else if( j == 10 )
							{
								if( ( (E判定文字表示位置) CDTXMania.ConfigIni.判定文字表示位置.Guitar ) == E判定文字表示位置.表示OFF )
								{
									// goto Label_06B7;
									continue;
								}
								num5 = ( ( (E判定文字表示位置) CDTXMania.ConfigIni.判定文字表示位置.Guitar ) == E判定文字表示位置.レーン上 ) ? 80 : 175;
								num6 = CDTXMania.ConfigIni.bReverse.Guitar ? 450 : 300;
							}

                            int nRectX = CDTXMania.ConfigIni.nJudgeWidgh;
                            int nRectY = CDTXMania.ConfigIni.nJudgeHeight;

                            int xc = (num5 + base.st状態[j].n相対X座標) + (this.stレーンサイズ[ j ].w / 2);
                            int x = (xc - ((int)((110f * base.st状態[j].fX方向拡大率) * ((j < 10) ? 1.0 : 0.7)))) - ((nRectX - 225) / 2);
                            int y = ((num6 + base.st状態[j].n相対Y座標) - ((int)(((140f * base.st状態[j].fY方向拡大率) * ((j < 10) ? 1.0 : 0.7)) / 2.0))) - ((nRectY - 135) / 2);
                            if (base.tx判定文字列[ index ] != null)
                            {
                                if (base.st状態[j].judge == E判定.Perfect)
                                {
                                    base.tx判定文字列[ index ].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                }
                                if (base.st状態[j].judge == E判定.Great)
                                {
                                    base.tx判定文字列[ index ].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 1, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                }
                                if (base.st状態[j].judge == E判定.Good)
                                {
                                    base.tx判定文字列[ index ].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 2, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                }
                                if (base.st状態[j].judge == E判定.Poor)
                                {
                                    base.tx判定文字列[ index ].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 3, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                }
                                if (base.st状態[j].judge == E判定.Miss)
                                {
                                    base.tx判定文字列[ index ].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 4, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                }
                                if (base.st状態[j].judge == E判定.Auto)
                                {
                                    base.tx判定文字列[ index ].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 5, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                }
								#region [ #25370 2011.6.3 yyagi ShowLag support ]
								if ( base.nShowLagType  == (int) EShowLagType.ON ||
									 ( ( base.nShowLagType == (int) EShowLagType.GREAT_POOR ) && ( base.st状態[ j ].judge != E判定.Perfect ) ) )
								{
									if ( base.st状態[ j ].judge != E判定.Auto && base.txlag数値 != null )		// #25370 2011.2.1 yyagi
									{
										bool minus = false;
										int offsetX = 0;
										string strDispLag = base.st状態[ j ].nLag.ToString();
										if ( st状態[ j ].nLag < 0 )
										{
											minus = true;
										}
										x = xc - strDispLag.Length * 15 / 2;
										for ( int i = 0; i < strDispLag.Length; i++ )
										{
											int p = ( strDispLag[ i ] == '-' ) ? 11 : (int) ( strDispLag[ i ] - '0' );	//int.Parse(strDispLag[i]);
											p += minus ? 0 : 12;		// change color if it is minus value
											base.txlag数値.t2D描画( CDTXMania.app.Device, x + offsetX, y + 35, base.stLag数値[ p ].rc );
											offsetX += 15;
										}
									}
								}
								#endregion
							}
						// Label_06B7: ;
						}
					}
				}
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		//[StructLayout( LayoutKind.Sequential )]
		//private struct STレーンサイズ
		//{
		//	public int x;
		//	public int w;
		//}

		//private STレーンサイズ[] stレーンサイズ;
		//-----------------
		#endregion
	}
}
