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
			if( !base.b活性化してない )
			{
				this.txゲージ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_gauge_bar.jpg" ) );
                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.D)
                {
                    this.txgbg = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Gauge.png"));
                    this.txgbar = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_bar.png"));

                    this.txゲージマスク = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Dummy.png"));
                    this.txゲージマスク2 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Dummy.png"));
                    this.txハイスピ = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Panel_icons.png"));
                }
                else
                {
                    this.txgbg = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_background.jpg"));
                    this.txgbar = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_bar.png"));
                    this.txgbar2 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_bar.png"));
                    this.txゲージマスク = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_mask.png"));
                    this.txゲージマスク2 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_mask2.png"));
                }
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                //テクスチャ 7枚
				CDTXMania.tテクスチャの解放( ref this.txゲージ );
                CDTXMania.tテクスチャの解放( ref this.txgbg);
                CDTXMania.tテクスチャの解放( ref this.txgbar);
                CDTXMania.tテクスチャの解放( ref this.txgbar2);
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
                    {
                        if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.D)
                        {
                            base.txgbg.t2D描画(CDTXMania.app.Device, 294, 626);
                        }
                        else
                        {
                            //A～C
                            base.txgbg.t2D描画(CDTXMania.app.Device, 0x102, (CDTXMania.ConfigIni.bReverse.Drums ? 20 : 655), new Rectangle(0, 0, 0x250, 0x2d));
                        }
                    }


                    if (base.dbゲージ値 > 0.0)
                    {
                        base.txgbar.vc拡大縮小倍率.X = (float)base.dbゲージ値;
                        if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.D)
                        {
                            if (base.db現在のゲージ値.Drums == 1.0)
                            {
                                base.txゲージ.t2D描画(CDTXMania.app.Device, 314, 635, new Rectangle(0, 0, 480, 31));
                            }
                            else
                            {
                                base.txgbar.t2D描画(CDTXMania.app.Device, 314, 635, new Rectangle(0, 0, 480, 31));
                            }
                        }
                        else
                        {
                            //A～C
                            base.txgbar.t2D描画(CDTXMania.app.Device, 0x12f, (CDTXMania.ConfigIni.bReverse.Drums ? 30 : 665), new Rectangle(0, 0, 0x1f8, 0x1a));
                        }
                    }
                    base.txgbar.vc拡大縮小倍率.X = 1f;
                    if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.D)
                    {
                        base.txgbar.t2D描画(CDTXMania.app.Device, 314, 635, new Rectangle(0, 31, 480, 31));
                    }
                    else
                    {
                        base.txgbar2.t2D描画(CDTXMania.app.Device, 0x133, (CDTXMania.ConfigIni.bReverse.Drums ? 30 : 0x299), new Rectangle(0, 0x1a, 0x1f8, 0x1a));
                    }

                }

				if ( base.b初めての進行描画 )
				{
					for( int k = 0; k < 0x18; k++ )
					{
						this.st白い星[ k ].x = 2 + CDTXMania.Random.Next( 4 );
						this.st白い星[ k ].fScale = 0.2f + ( CDTXMania.Random.Next( 2 ) * 0.05f );
						this.st白い星[ k ].ct進行 = new CCounter( 0, 0x160, 8 + CDTXMania.Random.Next( 4 ), CDTXMania.Timer );
						this.st白い星[ k ].ct進行.n現在の値 = CDTXMania.Random.Next( 0x160 );
					}
					this.ct本体移動 = new CCounter( 0, 0x1a, 20, CDTXMania.Timer );
					this.ct本体振動 = new CCounter( 0, 360, 4, CDTXMania.Timer );
					base.b初めての進行描画 = false;
				}

				int num2 = ( this.dbゲージ値 == 1.0 ) ? ( (int) ( 352.0 * this.dbゲージ値 ) ) : ( (int) ( ( 352.0 * this.dbゲージ値 ) + ( 2.0 * Math.Sin( Math.PI * 2 * ( ( (double) this.ct本体振動.n現在の値 ) / 360.0 ) ) ) ) );
				if( num2 <= 0 )
				{
					return 0;
				}
				int num3 = this.ct本体移動.n現在の値;
				int y = ( 0x195 - num2 ) - num3;
				int num5 = num2 + num3;
				while( num5 > 0 )
				{
					Rectangle rectangle = ( this.dbゲージ値 == 1.0 ) ? new Rectangle( 0x10, 0, 0x10, 0x1b ) : new Rectangle( 0, 0, 0x10, 0x1b );
					if( y < ( 0x195 - num2 ) )
					{
						int num6 = ( 0x195 - num2 ) - y;
						rectangle.Y += num6;
						rectangle.Height -= num6;
						y += num6;
					}
					if( ( y + rectangle.Height ) > 0x195 )
					{
						int num7 = ( y + rectangle.Height ) - 0x195;
						rectangle.Height -= num7;
					}
					if( rectangle.Top >= rectangle.Bottom )
					{
						break;
					}
					num5 -= rectangle.Height;
					y += rectangle.Height;
				}
				for( int i = 0; i < 4; i++ )
				{
					Rectangle rectangle2 = new Rectangle( 0x40 + ( i * 0x10 ), 0, 0x10, 0x40 );
					int num9 = ( 0x195 - num2 ) + ( i * 0x40 );
					if( num9 >= 0x195 )
					{
						break;
					}
					if( ( num9 + rectangle2.Height ) > 0x195 )
					{
						int num10 = ( num9 + rectangle2.Height ) - 0x195;
						rectangle2.Height -= num10;
					}
				}
				Rectangle rectangle3 = new Rectangle( 0x30, 0, 0x10, 0x10 );
				int num11 = 0x195 - num2;
				if( num11 < 0x195 )
				{
					if( ( num11 + rectangle3.Height ) > 0x195 )
					{
						int num12 = ( num11 + rectangle3.Height ) - 0x195;
						rectangle3.Height -= num12;
					}
				}
				for( int j = 0; j < 24; j++ )
				{
					this.st白い星[ j ].ct進行.t進行Loop();
					int x = 6 + this.st白い星[ j ].x;
					int num15 = ( 0x195 - num2 ) + ( 0x160 - this.st白い星[ j ].ct進行.n現在の値 );
					int num16 = ( this.st白い星[ j ].ct進行.n現在の値 < 0xb0 ) ? 0 : ( (int) ( 255.0 * ( ( (double) ( this.st白い星[ j ].ct進行.n現在の値 - 0xb0 ) ) / 176.0 ) ) );
					if( ( num16 != 0 ) && ( num15 < 0x191 ) )
					{
						Rectangle rectangle4 = new Rectangle( 0, 0x20, 0x20, 0x20 );
					}
				}
                if (base.db現在のゲージ値.Drums <= 0.3)
                {
                    this.txゲージマスク2.t2D描画(CDTXMania.app.Device, 259, (CDTXMania.ConfigIni.bReverse.Drums ? 17 :652));
                }
                if (base.db現在のゲージ値.Drums == 1.0)
                {
                    this.txゲージマスク.t2D描画(CDTXMania.app.Device, 259, (CDTXMania.ConfigIni.bReverse.Drums ? 17 : 652));
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
		//private CTexture txゲージ;
		//-----------------
		#endregion
	}
}
