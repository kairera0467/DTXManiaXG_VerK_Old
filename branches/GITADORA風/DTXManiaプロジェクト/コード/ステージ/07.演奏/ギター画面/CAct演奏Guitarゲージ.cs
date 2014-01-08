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

                    #region [ ギターのゲージ ]
                    if (base.txgbg != null)
                    {
                        base.txgbg.t2D描画(CDTXMania.app.Device, 76, 0, new Rectangle(0, 0, 349, 42));
                        if (base.dbゲージ値 > 0.0)
                        {
                            base.txgbar.vc拡大縮小倍率.X = (float)base.dbゲージ値;
                            base.txgbar.t2D描画(CDTXMania.app.Device, 89, 8, new Rectangle(0, 0, 318, 26));
                        }
                        base.txgbar.vc拡大縮小倍率.X = 1f;
                        base.txgbar.t2D描画(CDTXMania.app.Device, 89, 8, new Rectangle(0, 0x1a, 0x1f8, 0x1a));
                    }
                    #endregion

                    #region [ ベースのゲージ ]
                    if (base.txgbg != null)
                    {
                        base.txgbg.t2D描画(CDTXMania.app.Device, 820, 0, new Rectangle(0, 0, 349, 42));
                        if (base.dbゲージ値 > 0.0)
                        {
                            base.txgbar.vc拡大縮小倍率.X = (float)base.dbゲージ値;
                            base.txgbar.t2D描画(CDTXMania.app.Device, 833, 8, new Rectangle(0, 0, 318, 26));
                        }
                        base.txgbar.vc拡大縮小倍率.X = 1f;
                        base.txgbar.t2D描画(CDTXMania.app.Device, 833, 8, new Rectangle(0, 0x1a, 0x1f8, 0x1a));
                    }
                    #endregion

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
