﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CActSelectArtistComment : CActivity
	{
		// メソッド

		public CActSelectArtistComment()
		{
			base.b活性化してない = true;
		}
		public void t選択曲が変更された()
		{
			Cスコア cスコア = CDTXMania.stage選曲.r現在選択中のスコア;
			if( cスコア != null )
			{
				Bitmap image = new Bitmap( 1, 1 );
                /*
				CDTXMania.tテクスチャの解放( ref this.txArtist );
				this.strArtist = cスコア.譜面情報.アーティスト名;
				if( ( this.strArtist != null ) && ( this.strArtist.Length > 0 ) )
				{
					Graphics graphics = Graphics.FromImage( image );
					graphics.PageUnit = GraphicsUnit.Pixel;
					SizeF ef = graphics.MeasureString( this.strArtist, this.ft描画用フォント );
					graphics.Dispose();
					if (ef.Width > SampleFramework.GameWindowSize.Width)
					{
						ef.Width = SampleFramework.GameWindowSize.Width;
					}
					try
					{
						Bitmap bitmap2 = new Bitmap( (int) Math.Ceiling( (double) ef.Width ), (int) Math.Ceiling( (double) this.ft描画用フォント.Size ) );
						graphics = Graphics.FromImage( bitmap2 );
						graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
						graphics.DrawString( this.strArtist, this.ft描画用フォント, Brushes.White, ( float ) 0f, ( float ) 0f );
						graphics.Dispose();
						this.txArtist = new CTexture( CDTXMania.app.Device, bitmap2, CDTXMania.TextureFormat );
						this.txArtist.vc拡大縮小倍率 = new Vector3( 0.5f, 0.5f, 1f );
						bitmap2.Dispose();
					}
					catch( CTextureCreateFailedException )
					{
						Trace.TraceError( "ARTISTテクスチャの生成に失敗しました。" );
						this.txArtist = null;
					}
				}
                */
				CDTXMania.tテクスチャの解放( ref this.txComment );
				this.strComment = cスコア.譜面情報.コメント;
				if( ( this.strComment != null ) && ( this.strComment.Length > 0 ) )
				{
					Graphics graphics2 = Graphics.FromImage( image );
					graphics2.PageUnit = GraphicsUnit.Pixel;
					SizeF ef2 = graphics2.MeasureString( this.strComment, this.ft描画用フォント );
					Size size = new Size( (int) Math.Ceiling( (double) ef2.Width ), (int) Math.Ceiling( (double) ef2.Height ) );
					graphics2.Dispose();
					this.nテクスチャの最大幅 = CDTXMania.app.Device.Capabilities.MaxTextureWidth;
					int maxTextureHeight = CDTXMania.app.Device.Capabilities.MaxTextureHeight;
					Bitmap bitmap3 = new Bitmap( size.Width, (int) Math.Ceiling( (double) this.ft描画用フォント.Size ) );
					graphics2 = Graphics.FromImage( bitmap3 );
					graphics2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
					graphics2.DrawString( this.strComment, this.ft描画用フォント, Brushes.White, ( float ) 0f, ( float ) 0f );
					graphics2.Dispose();
					this.nComment行数 = 1;
					this.nComment最終行の幅 = size.Width;
					while( this.nComment最終行の幅 > this.nテクスチャの最大幅 )
					{
						this.nComment行数++;
						this.nComment最終行の幅 -= this.nテクスチャの最大幅;
					}
					while( ( this.nComment行数 * ( (int) Math.Ceiling( (double) this.ft描画用フォント.Size ) ) ) > maxTextureHeight )
					{
						this.nComment行数--;
						this.nComment最終行の幅 = this.nテクスチャの最大幅;
					}
					Bitmap bitmap4 = new Bitmap( ( this.nComment行数 > 1 ) ? this.nテクスチャの最大幅 : this.nComment最終行の幅, this.nComment行数 * ( (int) Math.Ceiling( (double) this.ft描画用フォント.Size ) ) );
					graphics2 = Graphics.FromImage( bitmap4 );
					Rectangle srcRect = new Rectangle();
					Rectangle destRect = new Rectangle();
					for( int i = 0; i < this.nComment行数; i++ )
					{
						srcRect.X = i * this.nテクスチャの最大幅;
						srcRect.Y = 0;
						srcRect.Width = ( ( i + 1 ) == this.nComment行数 ) ? this.nComment最終行の幅 : this.nテクスチャの最大幅;
						srcRect.Height = bitmap3.Height;
						destRect.X = 0;
						destRect.Y = i * bitmap3.Height;
						destRect.Width = srcRect.Width;
						destRect.Height = srcRect.Height;
						graphics2.DrawImage( bitmap3, destRect, srcRect, GraphicsUnit.Pixel );
					}
					graphics2.Dispose();
					try
					{
						this.txComment = new CTexture( CDTXMania.app.Device, bitmap4, CDTXMania.TextureFormat );
						this.txComment.vc拡大縮小倍率 = new Vector3( 0.5f, 0.5f, 1f );
					}
					catch( CTextureCreateFailedException )
					{
						Trace.TraceError( "COMMENTテクスチャの生成に失敗しました。" );
						this.txComment = null;
					}
					bitmap4.Dispose();
					bitmap3.Dispose();
				}
				image.Dispose();
				if( this.txComment != null )
				{
					this.ctComment = new CCounter( -215, (int) ( ( ( ( this.nComment行数 - 1 ) * this.nテクスチャの最大幅 ) + this.nComment最終行の幅 ) * this.txComment.vc拡大縮小倍率.X ), 10, CDTXMania.Timer );
				}
			}
		}


		// CActivity 実装

		public override void On活性化()
		{
			this.ft描画用フォント = new Font( "MS PGothic", 35f, GraphicsUnit.Pixel );
            /*
			this.txArtist = null;
			this.strArtist = "";
            */
			this.txComment = null;
			this.strComment = "";
			this.nComment最終行の幅 = 0;
			this.nComment行数 = 0;
			this.nテクスチャの最大幅 = 0;
			this.ctComment = new CCounter();
			base.On活性化();
		}
		public override void On非活性化()
		{
//			CDTXMania.tテクスチャの解放( ref this.txArtist );
			CDTXMania.tテクスチャの解放( ref this.txComment );
			if( this.ft描画用フォント != null )
			{
				this.ft描画用フォント.Dispose();
				this.ft描画用フォント = null;
			}
			this.ctComment = null;
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.t選択曲が変更された();
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
//				CDTXMania.tテクスチャの解放( ref this.txArtist );
				CDTXMania.tテクスチャの解放( ref this.txComment );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
				if( this.ctComment.b進行中 )
				{
					this.ctComment.t進行Loop();
				}
                /*
				if( this.txArtist != null )
				{
					int x = 1260 - ( (int) ( this.txArtist.szテクスチャサイズ.Width * this.txArtist.vc拡大縮小倍率.X ) );		// #27648 2012.3.14 yyagi: -12 for scrollbar
					int y = 350;
					this.txArtist.t2D描画( CDTXMania.app.Device, x, y );
				}
                */

                if ( this.txComment != null && (this.txComment.szテクスチャサイズ.Width * this.txComment.vc拡大縮小倍率.X) < 215 )
                {
                    this.txComment.t2D描画(CDTXMania.app.Device, 560, 500);
                }
				else if( this.txComment != null )
				{

                    int num3 = 560;
                    int num4 = 500;
                    float num5 = this.txComment.vc拡大縮小倍率.X;

                    Rectangle rectangle = new Rectangle((int)(((float)this.ctComment.n現在の値)/num5), 0, (int)(215f / num5), (int)this.ft描画用フォント.Size);
                    if (rectangle.X < 0)
                    {
                        num3 -= (int) (rectangle.X * num5);
                        rectangle.Width += rectangle.X;
                        rectangle.X = 0;
                    }
                    if (rectangle.Right >= this.nComment最終行の幅)
                    {
                        rectangle.Width -= rectangle.Right - this.nComment最終行の幅;
                    }
                    this.txComment.t2D描画(CDTXMania.app.Device, num3, num4, rectangle);
				}
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		private CCounter ctComment;
		private Font ft描画用フォント;
		private int nComment行数;
		private int nComment最終行の幅;
		private const int nComment表示幅 = 510;
		private int nテクスチャの最大幅;
        /*
        private string strArtist;
		private CTexture txArtist;
        */
		private string strComment;
		private CTexture txComment;
		//-----------------
		#endregion
	}
}
