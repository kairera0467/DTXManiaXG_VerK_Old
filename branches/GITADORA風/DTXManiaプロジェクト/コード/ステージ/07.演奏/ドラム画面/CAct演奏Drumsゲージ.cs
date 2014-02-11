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
			base.On活性化();
		}
		public override void On非活性化()
		{
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
            if (!base.b活性化してない)
            {
                this.txgbg = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Gauge.png"));
                this.txgbar = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_bar.png"));
                this.txgbarf = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_bar.jpg"));

                this.txゲージマスク = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Dummy.png"));
                this.txゲージマスク2 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Dummy.png"));
                this.txハイスピ = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Panel_icons.jpg"));

                base.OnManagedリソースの作成();
            }
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                //テクスチャ 7枚
                CDTXMania.tテクスチャの解放( ref this.txgbg);
                CDTXMania.tテクスチャの解放( ref this.txgbar);
                CDTXMania.tテクスチャの解放( ref this.txgbarf);
                CDTXMania.tテクスチャの解放( ref this.txゲージマスク );
                CDTXMania.tテクスチャの解放( ref this.txゲージマスク2);
                if (this.txハイスピ != null)
                    CDTXMania.tテクスチャの解放( ref this.txハイスピ);
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{

			if ( !base.b活性化してない )
			{

                if (base.txgbg != null)
                {

                    base.txgbg.t2D描画(CDTXMania.app.Device, 294, (CDTXMania.ConfigIni.bReverse.Drums ? 28 : 626), new Rectangle(0, 0, 543, 47));
                    base.txハイスピ.vc拡大縮小倍率 = new Vector3(0.76190476190476190476190476190476f, 0.66666666666666666666666666666667f, 1.0f);
                    base.txハイスピ.t2D描画(CDTXMania.app.Device, 800, (CDTXMania.ConfigIni.bReverse.Drums ? 35 : 634), new Rectangle(0, CDTXMania.ConfigIni.n譜面スクロール速度.Drums * 48, 42, 48));
                    if (base.dbゲージ値 > 0.0)
                    {
                        base.txgbar.vc拡大縮小倍率.X = (float)base.dbゲージ値;
                        if (base.db現在のゲージ値.Drums == 1.0)
                        {
                            base.txgbarf.t2D描画(CDTXMania.app.Device, 314, (CDTXMania.ConfigIni.bReverse.Drums ? 37 : 635), new Rectangle(0, 0, 480, 31));
                        }
                        else
                        {
                            base.txgbar.t2D描画(CDTXMania.app.Device, 314, (CDTXMania.ConfigIni.bReverse.Drums ? 37 : 635), new Rectangle(0, 0, 480, 31));
                        }
                    }
                    base.txgbg.t2D描画(CDTXMania.app.Device, 294, (CDTXMania.ConfigIni.bReverse.Drums ? 28 : 626), new Rectangle(0, 47, 543, 47));
                }
                if (base.db現在のゲージ値.Drums <= 0.3)
                {
                    this.txゲージマスク2.t2D描画(CDTXMania.app.Device, 294, (CDTXMania.ConfigIni.bReverse.Drums ? 28 : 626));
                }
                if (base.db現在のゲージ値.Drums == 1.0)
                {
                    this.txゲージマスク.t2D描画(CDTXMania.app.Device, 294, (CDTXMania.ConfigIni.bReverse.Drums ? 28 : 626));
                }
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		[StructLayout( LayoutKind.Sequential )]
		private struct ST白い星
		{
			public int x;
			public float fScale;
			public CCounter ct進行;
		}
		//private CCounter ct本体振動;
		//private double dbゲージ値;
		private const int STAR_MAX = 0x18;
		private ST白い星[] st白い星 = new ST白い星[ 0x18 ];
		//-----------------
		#endregion
	}
}
