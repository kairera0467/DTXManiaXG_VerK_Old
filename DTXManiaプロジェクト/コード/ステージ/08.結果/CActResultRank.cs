using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CActResultRank : CActivity
	{
		// コンストラクタ

		public CActResultRank()
		{
			base.b活性化してない = true;
		}


		// メソッド

		public void tアニメを完了させる()
		{
			this.ctランク表示.n現在の値 = this.ctランク表示.n終了値;
		}


		// CActivity 実装

		public override void On活性化()
		{
			this.n本体X = 138;
			this.n本体Y = 8;
			base.On活性化();
		}
		public override void On非活性化()
		{
			if( this.ctランク表示 != null )
			{
				this.ctランク表示 = null;
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.txランクパネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenResult rank panel.png" ) );
				switch ( CDTXMania.stage結果.n総合ランク値 )
				{
					case 0:
						this.txランク文字 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_rankSS.png" ) );
						break;

					case 1:
						this.txランク文字 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_rankS.png" ) );
						break;

					case 2:
						this.txランク文字 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_rankA.png" ) );
						break;

					case 3:
						this.txランク文字 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_rankB.png" ) );
						break;

					case 4:
						this.txランク文字 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_rankC.png" ) );
						break;

					case 5:
						this.txランク文字 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_rankD.png" ) );
						break;

					case 6:
					case 99:	// #23534 2010.10.28 yyagi: 演奏チップが0個のときは、rankEと見なす
						this.txランク文字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankE.png"));
						break;

					default:
						this.txランク文字 = null;
						break;
				}
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txランクパネル );
				CDTXMania.tテクスチャの解放( ref this.txランク文字 );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( base.b活性化してない )
			{
				return 0;
			}
			if( base.b初めての進行描画 )
			{
				this.ctランク表示 = new CCounter( 0, 0x514, 2, CDTXMania.Timer );
				base.b初めての進行描画 = false;
			}
			this.ctランク表示.t進行();
			if( this.ctランク表示.n現在の値 >= 700 )
			{
				float y = ( this.ctランク表示.n現在の値 > 0x3e8 ) ? 1f : ( ( (float) ( this.ctランク表示.n現在の値 - 700 ) ) / 300f );
				if( this.txランクパネル != null )
				{
					this.txランクパネル.n透明度 = (int) ( 255f * y );
					this.txランクパネル.vc拡大縮小倍率 = new Vector3( 1f, y, 1f );
					this.txランクパネル.t2D描画( CDTXMania.app.Device, this.n本体X, this.n本体Y + ( (int) ( ( 1f - y ) * 64f ) ) );
				}
			}
			if( this.ctランク表示.n現在の値 >= 0x3e8 )
			{
				double num2 = ( (double) ( this.ctランク表示.n現在の値 - 0x3e8 ) ) / 300.0;
				if( this.txランク文字 != null )
				{
					this.txランク文字.t2D描画( CDTXMania.app.Device, this.n本体X, this.n本体Y, new Rectangle( 0, 0, (int) ( 274.0 * num2 ), 274 ) );
				}
			}
			if( !this.ctランク表示.b終了値に達した )
			{
				return 0;
			}
			return 1;
		}
		

		// その他

		#region [ private ]
		//-----------------
		private CCounter ctランク表示;
		private int n本体X;
		private int n本体Y;
		private CTexture txランクパネル;
		private CTexture txランク文字;
		//-----------------
		#endregion
	}
}
