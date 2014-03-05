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
		
		// コンストラクタ

		public CAct演奏Drumsゲージ()
		{
			base.b活性化してない = true;
		}


		// CActivity 実装

		public override void On活性化()
		{
            base.n本体X.Drums = 0x102; //もとの数値は0x102、中央寄せする場合は0x116。
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
                this.txフレーム = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Gauge.png"));
                this.txゲージ = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_bar.png"));
                this.txフルゲージ = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_bar.jpg"));
                this.txマスクF = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_mask.png"));
                this.txマスクD = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_mask2.png"));

                this.nゲージX = (int)((float)(base.txフレーム.sz画像サイズ.Width - base.txゲージ.sz画像サイズ.Width) / 2f);
                this.nフルゲージX = (int)((float)(base.txフレーム.sz画像サイズ.Width - base.txフルゲージ.sz画像サイズ.Width) / 2f);

				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                //テクスチャ 7枚
                CDTXMania.tテクスチャの解放( ref this.txフレーム );
                CDTXMania.tテクスチャの解放( ref this.txゲージ );
                CDTXMania.tテクスチャの解放( ref this.txフルゲージ );
                CDTXMania.tテクスチャの解放( ref this.txマスクF );
                CDTXMania.tテクスチャの解放( ref this.txマスクD );
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
                if( base.txフレーム != null & base.txゲージ != null )
                {
                    //A～C
                    base.txフレーム.t2D描画(CDTXMania.app.Device, base.n本体X.Drums, (CDTXMania.ConfigIni.bReverse.Drums ? 20 : 655), new Rectangle(0, 0, base.txフレーム.sz画像サイズ.Width, 0x2d));

                    if (base.db現在のゲージ値.Drums == 1.0)
                    {
                        base.txフルゲージ.t2D描画(CDTXMania.app.Device, 1 + this.nフルゲージX + base.n本体X.Drums, (CDTXMania.ConfigIni.bReverse.Drums ? 30 : 665), new Rectangle(0, 0, base.txフルゲージ.sz画像サイズ.Width, 0x1a));
                    }
                    else if (base.db現在のゲージ値.Drums >= 0.0)
                    {
                        base.txゲージ.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Drums;
                        base.txゲージ.t2D描画(CDTXMania.app.Device, 1 + this.nゲージX + base.n本体X.Drums, (CDTXMania.ConfigIni.bReverse.Drums ? 30 : 665), new Rectangle(0, 0, base.txゲージ.sz画像サイズ.Width, 0x1a));
                    }

                    base.txフレーム.t2D描画(CDTXMania.app.Device, base.n本体X.Drums, (CDTXMania.ConfigIni.bReverse.Drums ? 20 : 655), new Rectangle(0, 0x2d, base.txフレーム.sz画像サイズ.Width, 0x2d));
                }

                if( base.IsDanger( E楽器パート.DRUMS ) && base.db現在のゲージ値.Drums >= 0.0 && this.txマスクD != null )
                {
                    this.txマスクD.t2D描画( CDTXMania.app.Device, 0x01 + base.n本体X.Drums, ( CDTXMania.ConfigIni.bReverse.Drums ? 17 : 652 ));
                }
                if( base.db現在のゲージ値.Drums == 1.0 && base.txマスクF != null )
                {
                    this.txマスクF.t2D描画(CDTXMania.app.Device, 0x01 + base.n本体X.Drums, (CDTXMania.ConfigIni.bReverse.Drums ? 17 : 652));
                    this.txマスクF.n透明度 = ( this.ct本体移動.n現在の値 <= 750 ? (int)( this.ct本体移動.n現在の値 / 2.94 ) : 500 - (int)(( this.ct本体移動.n現在の値) / 2.94 ) );
                }
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
        private int nゲージX;
        private int nフルゲージX;
		//-----------------
		#endregion
	}
}
