using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Drumsゲージ : CAct演奏ゲージ共通
	{
		// プロパティ

//		public double db現在のゲージ値
//		{
//			get
//			{
//				return this.dbゲージ値;
//			}
//			set
//			{
//				this.dbゲージ値 = value;
//				if( this.dbゲージ値 > 1.0 )
//				{
//					this.dbゲージ値 = 1.0;
//				}
//			}
//		}

		
		// コンストラクタ

		public CAct演奏Drumsゲージ()
		{
			base.b活性化してない = true;
		}


		// CActivity 実装

		public override void On活性化()
		{
            this.n本体X = 0x102; //もとの数値は0x102、中央寄せする場合は0x116。
            this.ct本体移動 = new CCounter( 0, 1500, 2, CDTXMania.Timer );
			base.On活性化();
		}
		public override void On非活性化()
		{
            this.ct本体移動 = null;
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.txgbg = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Gauge.png"));
                this.txgbar = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_bar.png"));
                this.txgbarf = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_bar.jpg"));
                this.txゲージマスク = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_mask.png"));
                this.txゲージマスク2 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_mask2.png"));
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                //テクスチャ 7枚
                CDTXMania.tテクスチャの解放( ref this.txgbg );
                CDTXMania.tテクスチャの解放( ref this.txgbar );
                CDTXMania.tテクスチャの解放( ref this.txgbarf );
                CDTXMania.tテクスチャの解放( ref this.txゲージマスク );
                CDTXMania.tテクスチャの解放( ref this.txゲージマスク2 );
                if (this.txハイスピ != null)
                    CDTXMania.tテクスチャの解放( ref this.txハイスピ );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if ( !base.b活性化してない )
			{
                this.ct本体移動.t進行Loop();
                if (base.txgbg != null)
                {
                    //A～C
                    base.txgbg.t2D描画(CDTXMania.app.Device, this.n本体X, (CDTXMania.ConfigIni.bReverse.Drums ? 20 : 655), new Rectangle(0, 0, base.txgbg.sz画像サイズ.Width, 0x2d));

                    if ( base.dbゲージ値 == 1.0)
                    {
                        base.txgbarf.vc拡大縮小倍率.X = (float)base.dbゲージ値;
                        base.txgbarf.t2D描画(CDTXMania.app.Device, 0x2d + this.n本体X, (CDTXMania.ConfigIni.bReverse.Drums ? 30 : 665), new Rectangle(0, 0, base.txgbarf.sz画像サイズ.Width, 0x1a));
                    }
                    else if (base.dbゲージ値 > 0.0)
                    {
                        base.txgbar.vc拡大縮小倍率.X = (float)base.dbゲージ値;
                        base.txgbar.t2D描画(CDTXMania.app.Device, 0x2d + this.n本体X, (CDTXMania.ConfigIni.bReverse.Drums ? 30 : 665), new Rectangle(0, 0, base.txgbar.sz画像サイズ.Width, 0x1a));
                    }

                    base.txgbg.t2D描画(CDTXMania.app.Device, this.n本体X, (CDTXMania.ConfigIni.bReverse.Drums ? 20 : 655), new Rectangle(0, 0x2d, base.txgbg.sz画像サイズ.Width, 0x2d));
                }

                if (base.IsDanger(E楽器パート.DRUMS) && base.db現在のゲージ値.Drums >= 0.0)
                {
                    this.txゲージマスク2.t2D描画( CDTXMania.app.Device, 0x01 + this.n本体X, ( CDTXMania.ConfigIni.bReverse.Drums ? 17 : 652 ));
                }
                if (base.db現在のゲージ値.Drums == 1.0)
                {
                    this.txゲージマスク.t2D描画(CDTXMania.app.Device, 0x01 + this.n本体X, (CDTXMania.ConfigIni.bReverse.Drums ? 17 : 652));
                    this.txゲージマスク.n透明度 = ( this.ct本体移動.n現在の値 <= 750 ? (int)( this.ct本体移動.n現在の値 / 2.94 ) : 500 - (int)(( this.ct本体移動.n現在の値) / 2.94 ) );
                }
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
        private int n本体X;
		//-----------------
		#endregion
	}
}
