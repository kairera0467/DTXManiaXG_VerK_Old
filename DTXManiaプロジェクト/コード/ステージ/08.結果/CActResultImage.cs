using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;
using FDK;

namespace DTXMania
{
	internal class CActResultImage : CActivity
	{
		// コンストラクタ
        /// <summary>
        /// リザルト画像を表示させるクラス。XG化するにあたって動画は廃止。
        /// また、中央の画像も表示する。(STAGE表示、STANDARD・CLASSICなど)
        /// </summary>
		public CActResultImage()
		{
			base.b活性化してない = true;
		}


		// メソッド

		public void tアニメを完了させる()
		{
			this.ct登場用.n現在の値 = this.ct登場用.n終了値;
		}


		// CActivity 実装

		public override void On活性化()
		{
			this.n本体X = 4;
			this.n本体Y = 0x3f;
			base.On活性化();
		}
		public override void On非活性化()
		{
			if( this.ct登場用 != null )
			{
				this.ct登場用 = null;
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.txパネル本体 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_resultimage panel.png" ) );
				this.txリザルト画像がないときの画像 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_preimage default.png" ) );
                this.tx中央パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_center panel.png" ) );
				if( CDTXMania.ConfigIni.bストイックモード )
				{
					this.r表示するリザルト画像 = this.txリザルト画像がないときの画像;
				}
				else if( ( !this.tリザルト画像の指定があれば構築する() ) && !this.tプレビュー画像の指定があれば構築する() )
				{
					this.r表示するリザルト画像 = this.txリザルト画像がないときの画像;
				}
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                CDTXMania.tテクスチャの解放( ref this.tx中央パネル );
				CDTXMania.tテクスチャの解放( ref this.txパネル本体 );
				CDTXMania.tテクスチャの解放( ref this.txリザルト画像 );
				CDTXMania.tテクスチャの解放( ref this.txリザルト画像がないときの画像 );
				base.OnManagedリソースの解放();
			}
		}
		public override unsafe int On進行描画()
		{
			if( base.b活性化してない )
			{
				return 0;
			}
			if( base.b初めての進行描画 )
			{
				this.ct登場用 = new CCounter( 0, 100, 5, CDTXMania.Timer );
				base.b初めての進行描画 = false;
			}
			this.ct登場用.t進行();
			if( this.ct登場用.b終了値に達した )
			{
				this.n本体X = 4;
				this.n本体Y = 0x3f;
			}
			else
			{
				double num3 = ( (double) this.ct登場用.n現在の値 ) / 100.0;
				double num4 = Math.Cos( ( 1.5 + ( 0.5 * num3 ) ) * Math.PI );
				this.n本体X = 4;
				this.n本体Y = 0x3f - ( (int) ( ( ( this.txパネル本体 != null ) ? ( (double) this.txパネル本体.sz画像サイズ.Height ) : ( (double) 0 ) ) * ( 1.0 - ( num4 * num4 ) ) ) );
			}
            if( this.tx中央パネル != null )
            {
                this.tx中央パネル.t2D描画( CDTXMania.app.Device, 0, 267 );
            }

			if( this.txパネル本体 != null )
			{
				this.txパネル本体.t2D描画( CDTXMania.app.Device, this.n本体X, this.n本体Y );
			}
			int x = 449;
			int y = 284;
			if( this.r表示するリザルト画像 != null )
			{
				int width = this.r表示するリザルト画像.szテクスチャサイズ.Width;
				int height = this.r表示するリザルト画像.szテクスチャサイズ.Height;
				if( width > 400 )
				{
					width = 400;
				}
				if( height > 400 )
				{
					height = 400;
				}
                this.r表示するリザルト画像.vc拡大縮小倍率.X = ((float)102) / ((float)width);
                this.r表示するリザルト画像.vc拡大縮小倍率.Y = ((float)102) / ((float)height);

				this.r表示するリザルト画像.t2D描画( CDTXMania.app.Device, x, y, new Rectangle( 0, 0, width, height ) );
			}
			if( !this.ct登場用.b終了値に達した )
			{
				return 0;
			}
			return 1;
		}


		// その他

		#region [ private ]
		//-----------------
		private CCounter ct登場用;
		private int n本体X;
		private int n本体Y;
		private CTexture r表示するリザルト画像;
		private CTexture txパネル本体;
        private CTexture tx中央パネル;
		private CTexture txリザルト画像;
		private CTexture txリザルト画像がないときの画像;

		private bool t背景画像があればその一部からリザルト画像を構築する()
		{
			string bACKGROUND;
			if( ( CDTXMania.ConfigIni.bギタレボモード && ( CDTXMania.DTX.BACKGROUND_GR != null ) ) && ( CDTXMania.DTX.BACKGROUND_GR.Length > 0 ) )
			{
				bACKGROUND = CDTXMania.DTX.BACKGROUND_GR;
			}
			else
			{
				bACKGROUND = CDTXMania.DTX.BACKGROUND;
			}
			if( string.IsNullOrEmpty( bACKGROUND ) )
			{
				return false;
			}
			CDTXMania.tテクスチャの解放( ref this.txリザルト画像 );
			this.r表示するリザルト画像 = null;
			bACKGROUND = CDTXMania.DTX.strフォルダ名 + bACKGROUND;
			Bitmap image = null;
			Bitmap bitmap2 = null;
			Bitmap bitmap3 = null;
			try
			{
				image = new Bitmap( bACKGROUND );
				bitmap2 = new Bitmap(SampleFramework.GameWindowSize.Width, SampleFramework.GameWindowSize.Height);
				Graphics graphics = Graphics.FromImage( bitmap2 );
				int x = 0;
				for (int i = 0; i < SampleFramework.GameWindowSize.Height; i += image.Height)
				{
					for (x = 0; x < SampleFramework.GameWindowSize.Width; x += image.Width)
					{
						graphics.DrawImage( image, x, i, image.Width, image.Height );
					}
				}
				graphics.Dispose();
				bitmap3 = new Bitmap( 0xcc, 0x10d );
				graphics = Graphics.FromImage( bitmap3 );
				graphics.DrawImage( bitmap2, 5, 5, new Rectangle( 0x157, 0x6d, 0xcc, 0x10d ), GraphicsUnit.Pixel );
				graphics.Dispose();
				this.txリザルト画像 = new CTexture( CDTXMania.app.Device, bitmap3, CDTXMania.TextureFormat );
				this.r表示するリザルト画像 = this.txリザルト画像;
			}
			catch
			{
				Trace.TraceError( "背景画像の読み込みに失敗しました。({0})", new object[] { bACKGROUND } );
				this.txリザルト画像 = null;
				return false;
			}
			finally
			{
				if( image != null )
				{
					image.Dispose();
				}
				if( bitmap2 != null )
				{
					bitmap2.Dispose();
				}
				if( bitmap3 != null )
				{
					bitmap3.Dispose();
				}
			}
			return true;
		}
		private bool tプレビュー画像の指定があれば構築する()
		{
			if( string.IsNullOrEmpty( CDTXMania.DTX.PREIMAGE ) )
			{
				return false;
			}
			CDTXMania.tテクスチャの解放( ref this.txリザルト画像 );
			this.r表示するリザルト画像 = null;
			string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PREIMAGE;
			if( !File.Exists( path ) )
			{
				Trace.TraceWarning( "ファイルが存在しません。({0})", new object[] { path } );
				return false;
			}
			this.txリザルト画像 = CDTXMania.tテクスチャの生成( path );
			this.r表示するリザルト画像 = this.txリザルト画像;
			return ( this.r表示するリザルト画像 != null );
		}
		private bool tリザルト画像の指定があれば構築する()
		{
			int rank = CScoreIni.t総合ランク値を計算して返す( CDTXMania.stage結果.st演奏記録.Drums, CDTXMania.stage結果.st演奏記録.Guitar, CDTXMania.stage結果.st演奏記録.Bass );
			if (rank == 99)	// #23534 2010.10.28 yyagi: 演奏チップが0個のときは、rankEと見なす
			{
				rank = 6;
			}
			if (string.IsNullOrEmpty(CDTXMania.DTX.RESULTIMAGE[rank]))
			{
				return false;
			}
			CDTXMania.tテクスチャの解放( ref this.txリザルト画像 );
			this.r表示するリザルト画像 = null;
			string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.RESULTIMAGE[ rank ];
			if( !File.Exists( path ) )
			{
				Trace.TraceWarning( "ファイルが存在しません。({0})", new object[] { path } );
				return false;
			}
			this.txリザルト画像 = CDTXMania.tテクスチャの生成( path );
			this.r表示するリザルト画像 = this.txリザルト画像;
			return ( this.r表示するリザルト画像 != null );
		}
		//-----------------
		#endregion
	}
}
