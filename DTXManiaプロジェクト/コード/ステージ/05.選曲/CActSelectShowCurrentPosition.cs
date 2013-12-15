﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CActSelectShowCurrentPosition : CActivity
	{
		// メソッド

		public CActSelectShowCurrentPosition()
		{
			base.b活性化してない = true;
		}

		// CActivity 実装

		public override void On活性化()
		{
			if ( this.b活性化してる )
				return;

			base.On活性化();
		}
		public override void On非活性化()
		{
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if ( !base.b活性化してない )
			{
				string pathScrollBar = CSkin.Path( @"Graphics\5_scrollbar.png" );
				string pathScrollPosition = CSkin.Path( @"Graphics\5_scrollbar.png" );
				if ( File.Exists( pathScrollBar ) )
				{
					this.txScrollBar = CDTXMania.tテクスチャの生成( pathScrollBar, false );
				}
				if ( File.Exists( pathScrollPosition ) )
				{
					this.txScrollPosition = CDTXMania.tテクスチャの生成( pathScrollPosition, false );
				}
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if ( !base.b活性化してない )
			{
				CDTXMania.t安全にDisposeする( ref this.txScrollBar );
				CDTXMania.t安全にDisposeする( ref this.txScrollPosition );

				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			#region [ スクロールバーの描画 #27648 ]
			if ( this.txScrollBar != null )
			{
				//for ( int sy = 0; sy < 336; sy += 128 )
				//{
				//	int ry = ( sy / 128 );
				//	int h = ( ( ry + 1 ) * 128 > 336 ) ? 336 - ry * 128 : 128;
				//	this.txScrollBar.t2D描画( CDTXMania.app.Device, 1280 - 12, 150 + sy, new Rectangle( ry * 12, 0, 12, h ) );	// 本当のy座標は88なんだが、なぜか約30のバイアスが掛かる・・・
				//}
                this.txScrollBar.t2D描画( CDTXMania.app.Device, (1280 - ((429.0f / 100.0f ) * CDTXMania.stage選曲.ct登場時アニメ用共通.n現在の値)), 164, new Rectangle( 0, 0, 352, 26 ) ); //移動後のxは851
			}
			#endregion
			#region [ スクロール地点の描画 (計算はCActSelect曲リストで行う。スクロール位置と選曲項目の同期のため。)#27648 ]
			if ( this.txScrollPosition != null )
			{
				int py = CDTXMania.stage選曲.nスクロールバー相対y座標;
				if( py <= 336 && py >= 0 )
				{
					this.txScrollPosition.t2D描画( CDTXMania.app.Device, ( 1280 - (( 424.0f / 100.0f ) * CDTXMania.stage選曲.ct登場時アニメ用共通.n現在の値 ) ) + py, 168, new Rectangle( 0, 26, 16, 16 ) );//856
				}
			}
			#endregion

			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		private CTexture txScrollPosition;
		private CTexture txScrollBar;
		//-----------------
		#endregion
	}
}
