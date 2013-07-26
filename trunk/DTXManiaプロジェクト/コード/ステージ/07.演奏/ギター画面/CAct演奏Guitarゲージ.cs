using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Guitarゲージ : CAct演奏ゲージ共通
	{
		// プロパティ

//		public STDGBVALUE<double> db現在のゲージ値;


		// コンストラクタ

		public CAct演奏Guitarゲージ()
		{
			base.b活性化してない = true;
		}


		// CActivity 実装

		public override void On活性化()
		{
			base.On活性化();
		}
		public override void On非活性化()
		{
			this.ct本体移動 = null;
			this.ct本体振動 = null;
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.txgbg = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Gauge_Guitar.png"));
                this.txgbar = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_bar.png"));
				this.txゲージ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayGuitar gauge.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txゲージ );
                CDTXMania.tテクスチャの解放( ref this.txgbar );
                CDTXMania.tテクスチャの解放(ref this.txgbar2);
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
				//int num9;
				if( base.b初めての進行描画 )
				{
					this.ct本体移動 = new CCounter( 0, 0x1a, 20, CDTXMania.Timer );
					this.ct本体振動 = new CCounter( 0, 360, 4, CDTXMania.Timer );
					base.b初めての進行描画 = false;
				}
				this.ct本体移動.t進行Loop();
				this.ct本体振動.t進行Loop();

                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                {
                    #region [ ギターのゲージ ]
                    if (base.txgbg != null && CDTXMania.DTX.bチップがある.Guitar)
                    {
                        base.txgbg.t2D描画(CDTXMania.app.Device, 76, 0, new Rectangle(0, 0, 349, 42));
                        if (base.dbゲージ値 > 0.0)
                        {
                            base.txgbar.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Guitar;
                            base.txgbar.t2D描画(CDTXMania.app.Device, 89, 8, new Rectangle(0, 0, 318, 26));
                        }
                        base.txgbar.vc拡大縮小倍率.X = 1f;
                        base.txgbar.t2D描画(CDTXMania.app.Device, 89, 8, new Rectangle(0, 0x1a, 0x1f8, 0x1a));
                    }
                    #endregion

                    #region [ ベースのゲージ ]
                    if (base.txgbg != null && CDTXMania.DTX.bチップがある.Bass)
                    {
                        base.txgbg.t2D描画(CDTXMania.app.Device, 864, 0, new Rectangle(0, 0, 349, 42));
                        if (base.dbゲージ値 > 0.0)
                        {
                            base.txgbar.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Bass;
                            base.txgbar.t2D描画(CDTXMania.app.Device, 877, 8, new Rectangle(0, 0, 318, 26));
                        }
                        base.txgbar.vc拡大縮小倍率.X = 1f;
                        base.txgbar.t2D描画(CDTXMania.app.Device, 877, 8, new Rectangle(0, 0x1a, 504, 0x1a));
                    }
                    #endregion
                }
                else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                {
                    #region [ ギターのゲージ ]
                    if ( base.txgbg != null && CDTXMania.DTX.bチップがある.Guitar )
                    {
                        base.txgbg.t2D描画(CDTXMania.app.Device, 0, 0, new Rectangle(0, 0, 349, 42));
                        if (base.dbゲージ値 > 0.0)
                        {
                            base.txgbar.vc拡大縮小倍率.X = (float)base.dbゲージ値;
                            base.txgbar.t2D描画(CDTXMania.app.Device, 13, 8, new Rectangle(0, 0, 318, 26));
                        }
                        base.txgbar.vc拡大縮小倍率.X = 1f;
                        base.txgbar.t2D描画(CDTXMania.app.Device, 13, 8, new Rectangle(0, 0x1a, 0x1f8, 0x1a));
                    }
                    #endregion
                    #region [ ベースのゲージ ]
                    if ( base.txgbg != null && CDTXMania.DTX.bチップがある.Bass )
                    {
                        base.txgbg.t2D描画(CDTXMania.app.Device, 938, 0, new Rectangle(0, 0, 349, 42));
                        if (base.dbゲージ値 > 0.0)
                        {
                            base.txgbar.vc拡大縮小倍率.X = (float)base.dbゲージ値;
                            base.txgbar.t2D描画(CDTXMania.app.Device, 938 +13, 8, new Rectangle(0, 0, 318, 26));
                        }
                        base.txgbar.vc拡大縮小倍率.X = 1f;
                        base.txgbar.t2D描画(CDTXMania.app.Device, 938 + 13, 8, new Rectangle(0, 0x1a, 0x1f8, 0x1a));
                    }
                    #endregion
                }

                /*
				#region [ ベースのゲージ ]
				if ( this.db現在のゲージ値.Bass == 1.0 )
				{
					num9 = (int) ( 128.0 * this.db現在のゲージ値.Bass );
				}
				else
				{
					num9 = (int) ( ( 128.0 * this.db現在のゲージ値.Bass ) + ( 2.0 * Math.Sin( Math.PI * 2 * ( ( (double) this.ct本体振動.n現在の値 ) / 360.0 ) ) ) );
				}
				if( num9 > 0 )
				{
					Rectangle rectangle2;
					int num10 = this.ct本体移動.n現在の値;
					int num11 = ( 0x1cf - num9 ) - num10;
					int num12 = num9 + num10;
					while( num12 > 0 )
					{
						if( this.db現在のゲージ値.Bass == 1.0 )
						{
							rectangle2 = new Rectangle( 10, 0x30, 0x1b, 0x10 );
						}
						else
						{
							rectangle2 = new Rectangle( 0x25, 0x30, 0x1b, 0x10 );
						}
						if( num11 < ( 0x1cf - num9 ) )
						{
							int num13 = ( 0x1cf - num9 ) - num11;
							rectangle2.X += num13;
							rectangle2.Width -= num13;
							num11 += num13;
						}
						if( ( num11 + rectangle2.Width ) > 0x1cf )
						{
							int num14 = ( num11 + rectangle2.Width ) - 0x1cf;
							rectangle2.Width -= num14;
						}
						if( rectangle2.Left >= rectangle2.Right )
						{
							break;
						}
						if( this.txゲージ != null )
						{
							this.txゲージ.b加算合成 = false;
							this.txゲージ.t2D描画( CDTXMania.app.Device, num11, 8, rectangle2 );
						}
						num12 -= rectangle2.Width;
						num11 += rectangle2.Width;
					}
					rectangle2 = new Rectangle( 0, 0x20, 0x40, 0x10 );
					num11 = 0x1cf - num9;
					if( ( num11 + rectangle2.Width ) > 0x1cf )
					{
						int num15 = ( num11 + rectangle2.Width ) - 0x1cf;
						rectangle2.Width -= num15;
					}
					if( ( rectangle2.Left < rectangle2.Right ) && ( this.txゲージ != null ) )
					{
						this.txゲージ.b加算合成 = true;
						this.txゲージ.t2D描画( CDTXMania.app.Device, num11, 8, rectangle2 );
					}
					if (this.bRisky && this.actLVLNFont != null)		// #23599 2011.7.30 yyagi Risky残りMiss回数表示
					{
						CActLVLNFont.EFontColor efc = this.IsDanger( E楽器パート.GUITAR ) ?
							CActLVLNFont.EFontColor.Red : CActLVLNFont.EFontColor.Yellow;
						actLVLNFont.t文字列描画( 445, 6, nRiskyTimes.ToString(), efc, CActLVLNFont.EFontAlign.Right);
					}
				}
				#endregion
                */
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		//private CCounter ct本体移動;
		//private CCounter ct本体振動;
		//private CTexture txゲージ;
		//-----------------
		#endregion
	}
}
