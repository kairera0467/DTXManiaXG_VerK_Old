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
        /// <summary>
        /// ゲージの描画クラス。ドラム側。
        /// 
        /// 課題
        /// ・ゲージの実装。
        /// ・Danger時にゲージの色が変わる演出の実装。
        /// ・Danger、MAX時のアニメーション実装。
        /// </summary>
		public CAct演奏Drumsゲージ()
		{
			base.b活性化してない = true;
		}


		// CActivity 実装

		public override void On活性化()
		{
			// CAct演奏ゲージ共通.Init()に移動
			// this.dbゲージ値 = ( CDTXMania.ConfigIni.nRisky > 0 ) ? 1.0 : 0.66666666666666663;
            this.ctマスク透明度タイマー = new CCounter(0, 1500, 2, CDTXMania.Timer);
			base.On活性化();
		}
		public override void On非活性化()
		{
            this.ctマスク透明度タイマー = null;
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.txゲージ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Gauge_drums.png" ) );
                this.txゲージ中身 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_gauge_bar.png" ) );
                this.txゲージマスクDANGER = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_gauge_mask_danger.png" ) );
                this.txゲージマスクMAX = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_gauge_mask_max.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txゲージ );
                CDTXMania.tテクスチャの解放( ref this.txゲージ中身 );
                CDTXMania.tテクスチャの解放( ref this.txゲージマスクDANGER );
                CDTXMania.tテクスチャの解放( ref this.txゲージマスクMAX );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if ( !base.b活性化してない )
			{
                this.ctマスク透明度タイマー.t進行Loop();

                if( this.txゲージ != null )
                {
                    //下地
                    this.txゲージ.t2D描画( CDTXMania.app.Device, 258, 656, new Rectangle( 0, 0, 592, 45 ) );

                    //ゲージ本体
                    this.txゲージ中身.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Drums;
                    this.txゲージ中身.t2D描画( CDTXMania.app.Device, 303, 666 );

                    //DANGER時のマスク画像
                    if( this.txゲージマスクDANGER != null && ( this.IsDanger( E楽器パート.DRUMS ) && base.db現在のゲージ値.Drums >= 0.0 ) )
                    {
                        this.txゲージマスクDANGER.t2D描画( CDTXMania.app.Device, 259, 629 );
                        this.txゲージマスクDANGER.n透明度 = ( this.ctマスク透明度タイマー.n現在の値 <= 750 ? (int)( this.ctマスク透明度タイマー.n現在の値 / 2.94 ) : 500 - (int)(( this.ctマスク透明度タイマー.n現在の値) / 2.94 ) );
                    }
                    //MAX時のマスク画像
                    if( this.txゲージマスクMAX != null && base.db現在のゲージ値.Drums == 1.0 )
                    {
                        this.txゲージマスクMAX.t2D描画( CDTXMania.app.Device, 259, 629 );
                        this.txゲージマスクMAX.n透明度 = ( this.ctマスク透明度タイマー.n現在の値 <= 750 ? (int)( this.ctマスク透明度タイマー.n現在の値 / 2.94 ) : 500 - (int)(( this.ctマスク透明度タイマー.n現在の値) / 2.94 ) );
                    }

                    //文字
                    this.txゲージ.t2D描画( CDTXMania.app.Device, 258, 656, new Rectangle( 0, 45, 592, 45 ) );
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

		//private CCounter ct本体移動;
		//private CCounter ct本体振動;
		//private double dbゲージ値;
		private const int STAR_MAX = 0x18;
		private ST白い星[] st白い星 = new ST白い星[ 0x18 ];
		//private CTexture txゲージ;
		//-----------------
		#endregion
	}
}
