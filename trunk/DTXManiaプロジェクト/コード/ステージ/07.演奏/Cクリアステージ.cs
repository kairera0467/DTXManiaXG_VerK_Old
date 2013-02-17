using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using SlimDX;
using SlimDX.Direct3D9;
using FDK;

namespace CDTXMania
{
	public class Cクリアステージ : CActivity
	{

		public override void On活性化( Device D3D9Device )
		{
			if( this.b活性化してる )
				return;

			this.ds背景動画 = DTXMania.CDTXMania.t失敗してもスキップ可能なDirectShowを生成する( DTXMania.CSkin.Path( @"Graphics\7_StageClear.mp4" ), DTXMania.CDTXMania.App.hWnd, true );

			base.On活性化( D3D9Device );
		}
		public override void On非活性化()
		{
			C共通.tDisposeする( this.ds背景動画 ); this.ds背景動画 = null;

			base.On非活性化();
		}
		public override void OnManagedリソース作成( Device D3D9Device )
		{
			if( this.b活性化してない )
				return;

			if( this.ds背景動画 != null )
			{
                this.tx背景動画 = DTXMania.CDTXMania.tテクスチャを生成する(this.ds背景動画.n幅px, this.ds背景動画.n高さpx);
			}
			else
				this.tx背景動画 = null;

			base.OnManagedリソース作成( D3D9Device );
		}
		public override void OnManagedリソースの解放()
		{
			if( this.b活性化してない )
				return;

			C共通.tDisposeする( ref this.tx背景動画 );

			base.OnManagedリソースの解放();
		}
		public override int On進行()
		{
			if( this.b活性化してない )
                return (int) DTXMania.CApp.E状態処理結果.NG;

			// 進行。

			#region [ 初めての進行処理。]
			//-----------------
			if( this.b初めての進行描画 )
			{
				if( this.ds背景動画 != null )
				{
					this.ds背景動画.bループ再生 = false;
					this.ds背景動画.t再生開始();
				}

				this.b初めての進行描画 = false;
			}
			//-----------------
			#endregion

            if(this.ds背景動画.b再生中 == true)
                return (int)DTXMania.CApp.E状態処理結果.NG;

            return (int)DTXMania.CApp.E状態処理結果.OK;
		}
		public override void On描画( Device D3D9Device )
		{
			if( this.b活性化してない )
				return;

			#region [ 背景動画 ]
			//-----------------
			if( this.ds背景動画 != null &&
				this.tx背景動画 != null )
			{

				this.ds背景動画.t現時点における最新のスナップイメージをTextureに転写する( this.tx背景動画 );
				this.tx背景動画.t2D描画( D3D9Device, 0, 0 );
			}
			//-----------------
			#endregion
		}

		#region [ protected ]
		//-----------------
		protected volatile CDirectShow ds背景動画 = null;
		protected CTexture tx背景動画 = null;
		//-----------------
		#endregion
	}
}
