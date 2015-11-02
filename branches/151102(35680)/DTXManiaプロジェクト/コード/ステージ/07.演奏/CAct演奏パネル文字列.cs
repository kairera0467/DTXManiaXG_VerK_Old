using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CAct演奏パネル文字列 : CActivity
	{
		// コンストラクタ

		public CAct演奏パネル文字列()
        {
			base.b活性化してない = true;
			this.strパネル文字列 = "";
		}
		
		
		// メソッド

		public void SetPanelString( string str )
		{
			this.strパネル文字列 = str;
			if( base.b活性化してる )
			{
				CDTXMania.tテクスチャの解放( ref this.txPanel );
				if( ( this.strパネル文字列 != null ) && ( this.strパネル文字列.Length > 0 ) )
				{
					Bitmap image = new Bitmap( 1, 1 );
					Graphics graphics = Graphics.FromImage( image );
					graphics.PageUnit = GraphicsUnit.Pixel;
					this.n文字列の長さdot = (int) graphics.MeasureString( this.strパネル文字列, this.ft表示用フォント ).Width;
					graphics.Dispose();
					try
					{
						Bitmap bitmap2 = new Bitmap( this.n文字列の長さdot, (int) this.ft表示用フォント.Size );
						graphics = Graphics.FromImage( bitmap2 );
						graphics.DrawString( this.strパネル文字列, this.ft表示用フォント, Brushes.White, (float) 0f, (float) 0f );
						graphics.Dispose();
						this.txPanel = new CTexture( CDTXMania.app.Device, bitmap2, CDTXMania.TextureFormat );
						this.txPanel.vc拡大縮小倍率 = new Vector3( 0.5f, 0.5f, 1f );
						bitmap2.Dispose();
					}
					catch( CTextureCreateFailedException )
					{
						Trace.TraceError( "パネル文字列テクスチャの生成に失敗しました。" );
						this.txPanel = null;
					}
					this.ct進行用 = new CCounter( -278, this.n文字列の長さdot / 2, 8, CDTXMania.Timer );
				}
			}
		}


		// CActivity 実装

		public override void On活性化()
		{
            this.ft表示用フォント = new Font("ＤＦＧ平成ゴシック体W7", 38f, FontStyle.Regular, GraphicsUnit.Pixel);
            this.ftSongNameFont = new System.Drawing.Font("ＤＦＧ平成ゴシック体W5", 20f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.n文字列の長さdot = 0;
			this.txPanel = null;
			this.ct進行用 = new CCounter();
			base.On活性化();
		}
		public override void On非活性化()
		{
			if( this.ft表示用フォント != null )
			{
				this.ft表示用フォント.Dispose();
				this.ft表示用フォント = null;
			}
			this.ct進行用 = null;
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.SetPanelString( this.strパネル文字列 );
                this.bmSongNameLength = new Bitmap(1, 1);
                Graphics graphics = Graphics.FromImage(this.bmSongNameLength);
                graphics.PageUnit = GraphicsUnit.Pixel;
                this.strSongName = string.IsNullOrEmpty( CDTXMania.DTX.TITLE ) ? "No Song Name" : ( CDTXMania.bコンパクトモード ? "---" : CDTXMania.stage選曲.r確定された曲.strタイトル);
                this.nSongNamePixelLength = (int)graphics.MeasureString(this.strSongName, this.ftSongNameFont).Width;
                graphics.Dispose();
                this.bmSongNameLength.Dispose();
                Bitmap image = new Bitmap(this.nSongNamePixelLength, (int)Math.Ceiling((double)this.ftSongNameFont.GetHeight()));
                graphics = Graphics.FromImage(image);
                graphics.DrawString(this.strSongName, this.ftSongNameFont, Brushes.White, (float)0f, (float)0f);
                graphics.Dispose();
                image.Dispose();
                this.ftSongNameFont.Dispose();

				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txPanel );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			throw new InvalidOperationException( "t進行描画(x,y)のほうを使用してください。" );
		}
        public int t進行描画(int x, int y)
		{
			if( !base.b活性化してない )
			{
				//this.ct進行用.t進行Loop();
				if( ( string.IsNullOrEmpty( this.strパネル文字列 ) || ( this.txPanel == null ) ) || ( this.ct進行用 == null ) )
				{
					return 0;
				}
				float num = this.txPanel.vc拡大縮小倍率.X;
				Rectangle rectangle = new Rectangle( (int) (   num ), 0, (int) ( 360f / num ), (int) this.ft表示用フォント.Size );
				if( rectangle.X < 0 )
				{
					x -= (int) ( rectangle.X * num );
					rectangle.Width += rectangle.X;
					rectangle.X = 0;
				}
				if( rectangle.Right >= this.n文字列の長さdot )
				{
					rectangle.Width -= rectangle.Right - this.n文字列の長さdot;
				}
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
        private Bitmap bmSongNameLength;
        private int nSongNamePixelLength;
		private CCounter ct進行用;
		private Font ft表示用フォント;
        private Font ftSongNameFont;
		private int n文字列の長さdot;
		private string strパネル文字列;
        private string strSongName;
		private CTexture txPanel;

		//-----------------
		#endregion
	}
}
