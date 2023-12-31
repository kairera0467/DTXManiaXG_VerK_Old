﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using SlimDX;
using SlimDX.Direct3D9;

namespace FDK
{
	public class CTexture : IDisposable
	{
		// プロパティ
		public bool b加算合成
		{
			get;
			set; 
		}
		public float fZ軸中心回転
		{
			get;
			set;
		}
		public int n透明度
		{
			get
			{
				return this._透明度;
			}
			set
			{
				if( value < 0 )
				{
					this._透明度 = 0;
				}
				else if( value > 0xff )
				{
					this._透明度 = 0xff;
				}
				else
				{
					this._透明度 = value;
				}
			}
		}
		public Size szテクスチャサイズ
		{
			get; 
			private set;
		}
		public Size sz画像サイズ
		{
			get;
			protected set;
		}
		public Texture texture
		{
			get;
			private set;
		}
		public Format Format
		{
			get;
			protected set;
		}
		public Vector3 vc拡大縮小倍率;
        public string filename;
        public string label; // DTXMania rev:2033adeeee1e46267cf36b50919c9ecee8804076

        	// 画面が変わるたび以下のプロパティを設定し治すこと。

        	public static Size sz論理画面 = Size.Empty;
        	public static Size sz物理画面 = Size.Empty;
        	public static Rectangle rc物理画面描画領域 = Rectangle.Empty;
        	/// <summary>
        	/// <para>論理画面を1とする場合の物理画面の倍率。</para>
        	/// <para>論理値×画面比率＝物理値。</para>
        	/// </summary>
        	public static float f画面比率 = 1.0f;

		// コンストラクタ

		public CTexture()
		{
			this.sz画像サイズ = new Size( 0, 0 );
			this.szテクスチャサイズ = new Size( 0, 0 );
			this._透明度 = 0xff;
			this.texture = null;
            this.bSlimDXTextureDispose完了済み = true;
			this.cvPositionColoredVertexies = null;
			this.b加算合成 = false;
			this.fZ軸中心回転 = 0f;
			this.vc拡大縮小倍率 = new Vector3( 1f, 1f, 1f );
            this.filename = ""; // DTXMania rev:693bf14b0d83efc770235c788117190d08a4e531
            this.label = ""; // DTXMania rev:2033adeeee1e46267cf36b50919c9ecee8804076
//			this._txData = null;
		}
		
		/// <summary>
		/// <para>指定されたビットマップオブジェクトから Managed テクスチャを作成する。</para>
		/// <para>テクスチャのサイズは、BITMAP画像のサイズ以上、かつ、D3D9デバイスで生成可能な最小のサイズに自動的に調節される。
		/// その際、テクスチャの調節後のサイズにあわせた画像の拡大縮小は行わない。</para>
		/// <para>その他、ミップマップ数は 1、Usage は None、Pool は Managed、イメージフィルタは Point、ミップマップフィルタは
		/// None、カラーキーは 0xFFFFFFFF（完全なる黒を透過）になる。</para>
		/// </summary>
		/// <param name="device">Direct3D9 デバイス。</param>
		/// <param name="bitmap">作成元のビットマップ。</param>
		/// <param name="format">テクスチャのフォーマット。</param>
		/// <exception cref="CTextureCreateFailedException">テクスチャの作成に失敗しました。</exception>
		public CTexture( Device device, Bitmap bitmap, Format format, string _label = "" )
			: this()
		{
			try
			{
				this.Format = format;
                this.label = _label;
				this.sz画像サイズ = new Size( bitmap.Width, bitmap.Height );
				this.szテクスチャサイズ = this.t指定されたサイズを超えない最適なテクスチャサイズを返す( device, this.sz画像サイズ );
				this.rc全画像 = new Rectangle( 0, 0, this.sz画像サイズ.Width, this.sz画像サイズ.Height );

				using( var stream = new MemoryStream() )
				{
					bitmap.Save( stream, ImageFormat.Bmp );
					stream.Seek( 0L, SeekOrigin.Begin );
					int colorKey = unchecked( (int) 0xFF000000 );
					this.texture = Texture.FromStream( device, stream, this.szテクスチャサイズ.Width, this.szテクスチャサイズ.Height, 1, Usage.None, format, poolvar, Filter.Point, Filter.None, colorKey );
                    this.bSlimDXTextureDispose完了済み = false;
				}
			}
			catch ( Exception e )
			{
				this.Dispose();
				throw new CTextureCreateFailedException( "ビットマップからのテクスチャの生成に失敗しました。(" + e.Message + ")" );
			}
		}
	
		/// <summary>
		/// <para>空の Managed テクスチャを作成する。</para>
		/// <para>テクスチャのサイズは、指定された希望サイズ以上、かつ、D3D9デバイスで生成可能な最小のサイズに自動的に調節される。
		/// その際、テクスチャの調節後のサイズにあわせた画像の拡大縮小は行わない。</para>
		/// <para>テクスチャのテクセルデータは未初期化。（おそらくゴミデータが入ったまま。）</para>
		/// <para>その他、ミップマップ数は 1、Usage は None、イメージフィルタは Point、ミップマップフィルタは None、
		/// カラーキーは 0x00000000（透過しない）になる。</para>
		/// </summary>
		/// <param name="device">Direct3D9 デバイス。</param>
		/// <param name="n幅">テクスチャの幅（希望値）。</param>
		/// <param name="n高さ">テクスチャの高さ（希望値）。</param>
		/// <param name="format">テクスチャのフォーマット。</param>
		/// <exception cref="CTextureCreateFailedException">テクスチャの作成に失敗しました。</exception>
		public CTexture( Device device, int n幅, int n高さ, Format format, string _label = "" )
			: this( device, n幅, n高さ, format, Pool.Managed, _label )
		{
		}
		
		/// <summary>
		/// <para>指定された画像ファイルから Managed テクスチャを作成する。</para>
		/// <para>利用可能な画像形式は、BMP, JPG, PNG, TGA, DDS, PPM, DIB, HDR, PFM のいずれか。</para>
		/// </summary>
		/// <param name="device">Direct3D9 デバイス。</param>
		/// <param name="strファイル名">画像ファイル名。</param>
		/// <param name="format">テクスチャのフォーマット。</param>
		/// <param name="b黒を透過する">画像の黒（0xFFFFFFFF）を透過させるなら true。</param>
		/// <exception cref="CTextureCreateFailedException">テクスチャの作成に失敗しました。</exception>
		public CTexture( Device device, string strファイル名, Format format, bool b黒を透過する, string _label = "" )
			: this( device, strファイル名, format, b黒を透過する, Pool.Managed, _label )
		{
		}
		public CTexture( Device device, byte[] txData, Format format, bool b黒を透過する, string _label = "" )
			: this( device, txData, format, b黒を透過する, Pool.Managed, _label )
		{
		}
		public CTexture( Device device, Bitmap bitmap, Format format, bool b黒を透過する, string _label = "" )
			: this( device, bitmap, format, b黒を透過する, Pool.Managed, _label )
		{
		}
		
		/// <summary>
		/// <para>空のテクスチャを作成する。</para>
		/// <para>テクスチャのサイズは、指定された希望サイズ以上、かつ、D3D9デバイスで生成可能な最小のサイズに自動的に調節される。
		/// その際、テクスチャの調節後のサイズにあわせた画像の拡大縮小は行わない。</para>
		/// <para>テクスチャのテクセルデータは未初期化。（おそらくゴミデータが入ったまま。）</para>
		/// <para>その他、ミップマップ数は 1、Usage は None、イメージフィルタは Point、ミップマップフィルタは None、
		/// カラーキーは 0x00000000（透過しない）になる。</para>
		/// </summary>
		/// <param name="device">Direct3D9 デバイス。</param>
		/// <param name="n幅">テクスチャの幅（希望値）。</param>
		/// <param name="n高さ">テクスチャの高さ（希望値）。</param>
		/// <param name="format">テクスチャのフォーマット。</param>
		/// <param name="pool">テクスチャの管理方法。</param>
		/// <exception cref="CTextureCreateFailedException">テクスチャの作成に失敗しました。</exception>
		public CTexture( Device device, int n幅, int n高さ, Format format, Pool pool, string _label = "" )
			: this( device, n幅, n高さ, format, pool, Usage.None, _label )
		{
		}
		
		public CTexture( Device device, int n幅, int n高さ, Format format, Pool pool, Usage usage, string _label = "" )
			: this()
		{
			try
			{
				this.Format = format;
                this.label = _label;
				this.sz画像サイズ = new Size( n幅, n高さ );
				this.szテクスチャサイズ = this.t指定されたサイズを超えない最適なテクスチャサイズを返す( device, this.sz画像サイズ );
				this.rc全画像 = new Rectangle( 0, 0, this.sz画像サイズ.Width, this.sz画像サイズ.Height );
		
				using ( var bitmap = new Bitmap( 1, 1 ) )
				{
					using ( var graphics = Graphics.FromImage( bitmap ) )
					{
						graphics.FillRectangle( Brushes.Black, 0, 0, 1, 1 );
					}
					using ( var stream = new MemoryStream() )
					{
						bitmap.Save( stream, ImageFormat.Bmp );
						stream.Seek( 0L, SeekOrigin.Begin );
#if TEST_Direct3D9Ex
						pool = poolvar;
#endif
						// 中で更にメモリ読み込みし直していて無駄なので、Streamを使うのは止めたいところ
						this.texture = Texture.FromStream( device, stream, n幅, n高さ, 1, usage, format, pool, Filter.Point, Filter.None, 0 );
                        this.bSlimDXTextureDispose完了済み = false;
					}
				}
			}
			catch
			{
				this.Dispose();
				throw new CTextureCreateFailedException( string.Format( "テクスチャの生成に失敗しました。\n({0}x{1}, {2})", n幅, n高さ, format ) );
			}
		}

		/// <summary>
		/// <para>画像ファイルからテクスチャを生成する。</para>
		/// <para>利用可能な画像形式は、BMP, JPG, PNG, TGA, DDS, PPM, DIB, HDR, PFM のいずれか。</para>
		/// <para>テクスチャのサイズは、画像のサイズ以上、かつ、D3D9デバイスで生成可能な最小のサイズに自動的に調節される。
		/// その際、テクスチャの調節後のサイズにあわせた画像の拡大縮小は行わない。</para>
		/// <para>その他、ミップマップ数は 1、Usage は None、イメージフィルタは Point、ミップマップフィルタは None になる。</para>
		/// </summary>
		/// <param name="device">Direct3D9 デバイス。</param>
		/// <param name="strファイル名">画像ファイル名。</param>
		/// <param name="format">テクスチャのフォーマット。</param>
		/// <param name="b黒を透過する">画像の黒（0xFFFFFFFF）を透過させるなら true。</param>
		/// <param name="pool">テクスチャの管理方法。</param>
		/// <exception cref="CTextureCreateFailedException">テクスチャの作成に失敗しました。</exception>
		public CTexture( Device device, string strファイル名, Format format, bool b黒を透過する, Pool pool, string _label = "" )
			: this()
		{
			MakeTexture( device, strファイル名, format, b黒を透過する, pool, _label );
		}
		public void MakeTexture( Device device, string strファイル名, Format format, bool b黒を透過する, Pool pool, string _label = "" )
		{
			if ( !File.Exists( strファイル名 ) )		// #27122 2012.1.13 from: ImageInformation では FileNotFound 例外は返ってこないので、ここで自分でチェックする。わかりやすいログのために。
				throw new FileNotFoundException( string.Format( "ファイルが存在しません。\n[{0}]", strファイル名 ) );

			Byte[] _txData = File.ReadAllBytes( strファイル名 );
            this.filename = Path.GetFileName( strファイル名 );
			MakeTexture( device, _txData, format, b黒を透過する, pool, _label );
		}

		public CTexture( Device device, byte[] txData, Format format, bool b黒を透過する, Pool pool, string _label = "" )
			: this()
		{
			MakeTexture( device, txData, format, b黒を透過する, pool, _label );
		}
		public void MakeTexture( Device device, byte[] txData, Format format, bool b黒を透過する, Pool pool, string _label = "" )
		{
			try
			{
				var information = ImageInformation.FromMemory( txData );
				this.Format = format;
                this.label = _label;
				this.sz画像サイズ = new Size( information.Width, information.Height );
				this.rc全画像 = new Rectangle( 0, 0, this.sz画像サイズ.Width, this.sz画像サイズ.Height );
				int colorKey = ( b黒を透過する ) ? unchecked( (int) 0xFF000000 ) : 0;
				this.szテクスチャサイズ = this.t指定されたサイズを超えない最適なテクスチャサイズを返す( device, this.sz画像サイズ );
#if TEST_Direct3D9Ex
				pool = poolvar;
#endif
				//				lock ( lockobj )
				//				{
				//Trace.TraceInformation( "CTexture() start: " );
				this.texture = Texture.FromMemory( device, txData, this.sz画像サイズ.Width, this.sz画像サイズ.Height, 1, Usage.None, format, pool, Filter.Point, Filter.None, colorKey );
                this.bSlimDXTextureDispose完了済み = false;
				//Trace.TraceInformation( "CTexture() end:   " );
				//				}
			}
			catch
			{
				this.Dispose();
				// throw new CTextureCreateFailedException( string.Format( "テクスチャの生成に失敗しました。\n{0}", strファイル名 ) );
				throw new CTextureCreateFailedException( string.Format( "テクスチャの生成に失敗しました。\n" ) );
			}
		}

		public CTexture( Device device, Bitmap bitmap, Format format, bool b黒を透過する, Pool pool, string _label = "" )
			: this()
		{
			MakeTexture( device, bitmap, format, b黒を透過する, pool, _label );
		}
		public void MakeTexture( Device device, Bitmap bitmap, Format format, bool b黒を透過する, Pool pool, string _label = "" )
		{
			try
			{
				this.Format = format;
                this.label = _label;
				this.sz画像サイズ = new Size( bitmap.Width, bitmap.Height );
				this.rc全画像 = new Rectangle( 0, 0, this.sz画像サイズ.Width, this.sz画像サイズ.Height );
				int colorKey = ( b黒を透過する ) ? unchecked( (int) 0xFF000000 ) : 0;
				this.szテクスチャサイズ = this.t指定されたサイズを超えない最適なテクスチャサイズを返す( device, this.sz画像サイズ );
#if TEST_Direct3D9Ex
				pool = poolvar;
#endif
				//Trace.TraceInformation( "CTExture() start: " );
				unsafe  // Bitmapの内部データ(a8r8g8b8)を自前でゴリゴリコピーする
				{
					int tw =
#if TEST_Direct3D9Ex
					288;		// 32の倍数にする(グラフによっては2のべき乗にしないとダメかも)
#else
					this.sz画像サイズ.Width;
#endif
#if TEST_Direct3D9Ex
					this.texture = new Texture( device, tw, this.sz画像サイズ.Height, 1, Usage.Dynamic, format, Pool.Default );
#else
					this.texture = new Texture( device, this.sz画像サイズ.Width, this.sz画像サイズ.Height, 1, Usage.None, format, pool );
#endif
					BitmapData srcBufData = bitmap.LockBits( new Rectangle( 0, 0, this.sz画像サイズ.Width, this.sz画像サイズ.Height ), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb );
					DataRectangle destDataRectangle = texture.LockRectangle( 0, LockFlags.Discard );	// None
#if TEST_Direct3D9Ex
					byte[] filldata = null;
					if ( tw > this.sz画像サイズ.Width )
					{
						filldata = new byte[ (tw - this.sz画像サイズ.Width) * 4 ];
					}
					for ( int y = 0; y < this.sz画像サイズ.Height; y++ )
					{
						IntPtr src_scan0 = (IntPtr) ( (Int64) srcBufData.Scan0 + y * srcBufData.Stride );
						destDataRectangle.Data.WriteRange( src_scan0, this.sz画像サイズ.Width * 4  );
						if ( tw > this.sz画像サイズ.Width )
						{
							destDataRectangle.Data.WriteRange( filldata );
						}
					}
#else
					IntPtr src_scan0 = (IntPtr) ( (Int64) srcBufData.Scan0 );
					destDataRectangle.Data.WriteRange( src_scan0, this.sz画像サイズ.Width * 4 * this.sz画像サイズ.Height );
#endif
					texture.UnlockRectangle( 0 );
					bitmap.UnlockBits( srcBufData );
                    this.bSlimDXTextureDispose完了済み = false;
				}
				//Trace.TraceInformation( "CTExture() End: " );
			}
			catch
			{
				this.Dispose();
				// throw new CTextureCreateFailedException( string.Format( "テクスチャの生成に失敗しました。\n{0}", strファイル名 ) );
				throw new CTextureCreateFailedException( string.Format( "テクスチャの生成に失敗しました。\n" ) );
			}
		}
		// メソッド

		/// <summary>
		/// テクスチャを 2D 画像と見なして描画する。
		/// </summary>
		/// <param name="device">Direct3D9 デバイス。</param>
		/// <param name="x">描画位置（テクスチャの左上位置の X 座標[dot]）。</param>
		/// <param name="y">描画位置（テクスチャの左上位置の Y 座標[dot]）。</param>
		public void t2D描画( Device device, int x, int y )
		{
			this.t2D描画( device, x, y, 1f, this.rc全画像 );
		}
		public void t2D描画( Device device, int x, int y, Rectangle rc画像内の描画領域 )
		{
			this.t2D描画( device, x, y, 1f, rc画像内の描画領域 );
		}
        public void t2D描画( Device device, float x, float y )
		{
			this.t2D描画( device, (int)x, (int)y, 1f, this.rc全画像 );
		}
		public void t2D描画( Device device, float x, float y, Rectangle rc画像内の描画領域 )
		{
			this.t2D描画( device, (int)x, (int)y, 1f, rc画像内の描画領域 );
		}
		public void t2D描画( Device device, int x, int y, float depth, Rectangle rc画像内の描画領域 )
		{
            if (this.texture == null)
                return;

			this.tレンダリングステートの設定( device );

			if( this.fZ軸中心回転 == 0f )
			{
				#region [ (A) 回転なし ]
				//-----------------
				float f補正値X = -0.5f;	// -0.5 は座標とピクセルの誤差を吸収するための座標補正値。(MSDN参照)
				float f補正値Y = -0.5f;	//
				float w = rc画像内の描画領域.Width;
				float h = rc画像内の描画領域.Height;
				float f左U値 = ( (float) rc画像内の描画領域.Left ) / ( (float) this.szテクスチャサイズ.Width );
				float f右U値 = ( (float) rc画像内の描画領域.Right ) / ( (float) this.szテクスチャサイズ.Width );
				float f上V値 = ( (float) rc画像内の描画領域.Top ) / ( (float) this.szテクスチャサイズ.Height );
				float f下V値 = ( (float) rc画像内の描画領域.Bottom ) / ( (float) this.szテクスチャサイズ.Height );
				this.color4.Alpha = ( (float) this._透明度 ) / 255f;
				int color = this.color4.ToArgb();

				if( this.cvTransformedColoredVertexies == null )
					this.cvTransformedColoredVertexies = new TransformedColoredTexturedVertex[ 4 ];

				// #27122 2012.1.13 from: 以下、マネージドオブジェクト（＝ガベージ）の量産を抑えるため、new は使わず、メンバに値を１つずつ直接上書きする。

				this.cvTransformedColoredVertexies[ 0 ].Position.X = x + f補正値X;
				this.cvTransformedColoredVertexies[ 0 ].Position.Y = y + f補正値Y;
				this.cvTransformedColoredVertexies[ 0 ].Position.Z = depth;
				this.cvTransformedColoredVertexies[ 0 ].Position.W = 1.0f;
				this.cvTransformedColoredVertexies[ 0 ].Color = color;
				this.cvTransformedColoredVertexies[ 0 ].TextureCoordinates.X = f左U値;
				this.cvTransformedColoredVertexies[ 0 ].TextureCoordinates.Y = f上V値;

				this.cvTransformedColoredVertexies[ 1 ].Position.X = ( x + ( w * this.vc拡大縮小倍率.X ) ) + f補正値X;
				this.cvTransformedColoredVertexies[ 1 ].Position.Y = y + f補正値Y;
				this.cvTransformedColoredVertexies[ 1 ].Position.Z = depth;
				this.cvTransformedColoredVertexies[ 1 ].Position.W = 1.0f;
				this.cvTransformedColoredVertexies[ 1 ].Color = color;
				this.cvTransformedColoredVertexies[ 1 ].TextureCoordinates.X = f右U値;
				this.cvTransformedColoredVertexies[ 1 ].TextureCoordinates.Y = f上V値;

				this.cvTransformedColoredVertexies[ 2 ].Position.X = x + f補正値X;
				this.cvTransformedColoredVertexies[ 2 ].Position.Y = ( y + ( h * this.vc拡大縮小倍率.Y ) ) + f補正値Y;
				this.cvTransformedColoredVertexies[ 2 ].Position.Z = depth;
				this.cvTransformedColoredVertexies[ 2 ].Position.W = 1.0f;
				this.cvTransformedColoredVertexies[ 2 ].Color = color;
				this.cvTransformedColoredVertexies[ 2 ].TextureCoordinates.X = f左U値;
				this.cvTransformedColoredVertexies[ 2 ].TextureCoordinates.Y = f下V値;

				this.cvTransformedColoredVertexies[ 3 ].Position.X = ( x + ( w * this.vc拡大縮小倍率.X ) ) + f補正値X;
				this.cvTransformedColoredVertexies[ 3 ].Position.Y = ( y + ( h * this.vc拡大縮小倍率.Y ) ) + f補正値Y;
				this.cvTransformedColoredVertexies[ 3 ].Position.Z = depth;
				this.cvTransformedColoredVertexies[ 3 ].Position.W = 1.0f;
				this.cvTransformedColoredVertexies[ 3 ].Color = color;
				this.cvTransformedColoredVertexies[ 3 ].TextureCoordinates.X = f右U値;
				this.cvTransformedColoredVertexies[ 3 ].TextureCoordinates.Y = f下V値;
				
				device.SetTexture( 0, this.texture );
				device.VertexFormat = TransformedColoredTexturedVertex.Format;
				device.DrawUserPrimitives( PrimitiveType.TriangleStrip, 0, 2, this.cvTransformedColoredVertexies );
				//-----------------
				#endregion
			}
			else
			{
				#region [ (B) 回転あり ]
				//-----------------
				float f補正値X = ( ( rc画像内の描画領域.Width % 2 ) == 0 ) ? -0.5f : 0f;	// -0.5 は座標とピクセルの誤差を吸収するための座標補正値。(MSDN参照)
				float f補正値Y = ( ( rc画像内の描画領域.Height % 2 ) == 0 ) ? -0.5f : 0f;	// 3D（回転する）なら補正はいらない。
				float f中央X = ( (float) rc画像内の描画領域.Width ) / 2f;
				float f中央Y = ( (float) rc画像内の描画領域.Height ) / 2f;
				float f左U値 = ( (float) rc画像内の描画領域.Left ) / ( (float) this.szテクスチャサイズ.Width );
				float f右U値 = ( (float) rc画像内の描画領域.Right ) / ( (float) this.szテクスチャサイズ.Width );
				float f上V値 = ( (float) rc画像内の描画領域.Top ) / ( (float) this.szテクスチャサイズ.Height );
				float f下V値 = ( (float) rc画像内の描画領域.Bottom ) / ( (float) this.szテクスチャサイズ.Height );
				this.color4.Alpha = ( (float) this._透明度 ) / 255f;
				int color = this.color4.ToArgb();

				if( this.cvPositionColoredVertexies == null )
					this.cvPositionColoredVertexies = new PositionColoredTexturedVertex[ 4 ];

				// #27122 2012.1.13 from: 以下、マネージドオブジェクト（＝ガベージ）の量産を抑えるため、new は使わず、メンバに値を１つずつ直接上書きする。

				this.cvPositionColoredVertexies[ 0 ].Position.X = -f中央X + f補正値X;
				this.cvPositionColoredVertexies[ 0 ].Position.Y = f中央Y + f補正値Y;
				this.cvPositionColoredVertexies[ 0 ].Position.Z = depth;
				this.cvPositionColoredVertexies[ 0 ].Color = color;
				this.cvPositionColoredVertexies[ 0 ].TextureCoordinates.X = f左U値;
				this.cvPositionColoredVertexies[ 0 ].TextureCoordinates.Y = f上V値;

				this.cvPositionColoredVertexies[ 1 ].Position.X = f中央X + f補正値X;
				this.cvPositionColoredVertexies[ 1 ].Position.Y = f中央Y + f補正値Y;
				this.cvPositionColoredVertexies[ 1 ].Position.Z = depth;
				this.cvPositionColoredVertexies[ 1 ].Color = color;
				this.cvPositionColoredVertexies[ 1 ].TextureCoordinates.X = f右U値;
				this.cvPositionColoredVertexies[ 1 ].TextureCoordinates.Y = f上V値;

				this.cvPositionColoredVertexies[ 2 ].Position.X = -f中央X + f補正値X;
				this.cvPositionColoredVertexies[ 2 ].Position.Y = -f中央Y + f補正値Y;
				this.cvPositionColoredVertexies[ 2 ].Position.Z = depth;
				this.cvPositionColoredVertexies[ 2 ].Color = color;
				this.cvPositionColoredVertexies[ 2 ].TextureCoordinates.X = f左U値;
				this.cvPositionColoredVertexies[ 2 ].TextureCoordinates.Y = f下V値;

				this.cvPositionColoredVertexies[ 3 ].Position.X = f中央X + f補正値X;
				this.cvPositionColoredVertexies[ 3 ].Position.Y = -f中央Y + f補正値Y;
				this.cvPositionColoredVertexies[ 3 ].Position.Z = depth;
				this.cvPositionColoredVertexies[ 3 ].Color = color;
				this.cvPositionColoredVertexies[ 3 ].TextureCoordinates.X = f右U値;
				this.cvPositionColoredVertexies[ 3 ].TextureCoordinates.Y = f下V値;

				int n描画領域内X = x + ( rc画像内の描画領域.Width / 2 );
				int n描画領域内Y = y + ( rc画像内の描画領域.Height / 2 );
				var vc3移動量 = new Vector3( n描画領域内X - ( ( (float) device.Viewport.Width ) / 2f ), -( n描画領域内Y - ( ( (float) device.Viewport.Height ) / 2f ) ), 0f );
				
				var matrix = Matrix.Identity * Matrix.Scaling( this.vc拡大縮小倍率 );
				matrix *= Matrix.RotationZ( this.fZ軸中心回転 );
				matrix *= Matrix.Translation( vc3移動量 );
				device.SetTransform( TransformState.World, matrix );

				device.SetTexture( 0, this.texture );
				device.VertexFormat = PositionColoredTexturedVertex.Format;
				device.DrawUserPrimitives( PrimitiveType.TriangleStrip, 2, this.cvPositionColoredVertexies );
				//-----------------
				#endregion
			}
		}
		public void t2D上下反転描画( Device device, int x, int y )
		{
			this.t2D上下反転描画( device, x, y, 1f, this.rc全画像 );
		}
		public void t2D上下反転描画( Device device, int x, int y, Rectangle rc画像内の描画領域 )
		{
			this.t2D上下反転描画( device, x, y, 1f, rc画像内の描画領域 );
		}
		public void t2D上下反転描画( Device device, int x, int y, float depth, Rectangle rc画像内の描画領域 )
		{
            if( this.texture == null )
				throw new InvalidOperationException( "テクスチャは生成されていません。" );

			this.tレンダリングステートの設定( device );

			float fx = x * CTexture.f画面比率 + CTexture.rc物理画面描画領域.X - 0.5f;	// -0.5 は座標とピクセルの誤差を吸収するための座標補正値。(MSDN参照)
			float fy = y * CTexture.f画面比率 + CTexture.rc物理画面描画領域.Y - 0.5f;	//
			float w = rc画像内の描画領域.Width * this.vc拡大縮小倍率.X * CTexture.f画面比率;
			float h = rc画像内の描画領域.Height * this.vc拡大縮小倍率.Y * CTexture.f画面比率;
			float f左U値 = ( (float) rc画像内の描画領域.Left ) / ( (float) this.szテクスチャサイズ.Width );
			float f右U値 = ( (float) rc画像内の描画領域.Right ) / ( (float) this.szテクスチャサイズ.Width );
			float f上V値 = ( (float) rc画像内の描画領域.Top ) / ( (float) this.szテクスチャサイズ.Height );
			float f下V値 = ( (float) rc画像内の描画領域.Bottom ) / ( (float) this.szテクスチャサイズ.Height );
			this.color4.Alpha = ( (float) this._透明度 ) / 255f;
			int color = this.color4.ToArgb();

            if( this.cvTransformedColoredVertexies == null )
			    this.cvTransformedColoredVertexies = new TransformedColoredTexturedVertex[ 4 ];

			// 以下、マネージドオブジェクトの量産を抑えるため new は使わない。

			this.cvTransformedColoredVertexies[ 0 ].TextureCoordinates.X = f左U値;	// 左上	→ 左下
			this.cvTransformedColoredVertexies[ 0 ].TextureCoordinates.Y = f下V値;
			this.cvTransformedColoredVertexies[ 0 ].Position.X = fx;
			this.cvTransformedColoredVertexies[ 0 ].Position.Y = fy;
			this.cvTransformedColoredVertexies[ 0 ].Position.Z = depth;
			this.cvTransformedColoredVertexies[ 0 ].Position.W = 1.0f;
			this.cvTransformedColoredVertexies[ 0 ].Color = color;

			this.cvTransformedColoredVertexies[ 1 ].TextureCoordinates.X = f右U値;	// 右上 → 右下
			this.cvTransformedColoredVertexies[ 1 ].TextureCoordinates.Y = f下V値;
			this.cvTransformedColoredVertexies[ 1 ].Position.X = fx + w;
			this.cvTransformedColoredVertexies[ 1 ].Position.Y = fy;
			this.cvTransformedColoredVertexies[ 1 ].Position.Z = depth;
			this.cvTransformedColoredVertexies[ 1 ].Position.W = 1.0f;
			this.cvTransformedColoredVertexies[ 1 ].Color = color;

			this.cvTransformedColoredVertexies[ 2 ].TextureCoordinates.X = f左U値;	// 左下 → 左上
			this.cvTransformedColoredVertexies[ 2 ].TextureCoordinates.Y = f上V値;
			this.cvTransformedColoredVertexies[ 2 ].Position.X = fx;
			this.cvTransformedColoredVertexies[ 2 ].Position.Y = fy + h;
			this.cvTransformedColoredVertexies[ 2 ].Position.Z = depth;
			this.cvTransformedColoredVertexies[ 2 ].Position.W = 1.0f;
			this.cvTransformedColoredVertexies[ 2 ].Color = color;

			this.cvTransformedColoredVertexies[ 3 ].TextureCoordinates.X = f右U値;	// 右下 → 右上
			this.cvTransformedColoredVertexies[ 3 ].TextureCoordinates.Y = f上V値;
			this.cvTransformedColoredVertexies[ 3 ].Position.X = fx + w;
			this.cvTransformedColoredVertexies[ 3 ].Position.Y = fy + h;
			this.cvTransformedColoredVertexies[ 3 ].Position.Z = depth;
			this.cvTransformedColoredVertexies[ 3 ].Position.W = 1.0f;
			this.cvTransformedColoredVertexies[ 3 ].Color = color;

			device.SetTexture( 0, this.texture );
			device.VertexFormat = TransformedColoredTexturedVertex.Format;
			device.DrawUserPrimitives( PrimitiveType.TriangleStrip, 2, this.cvTransformedColoredVertexies );
		}
		public void t2D上下反転描画( Device device, Point pt )
		{
			this.t2D上下反転描画( device, pt.X, pt.Y, 1f, this.rc全画像 );
		}
		public void t2D上下反転描画( Device device, Point pt, Rectangle rc画像内の描画領域 )
		{
			this.t2D上下反転描画( device, pt.X, pt.Y, 1f, rc画像内の描画領域 );
		}
		public void t2D上下反転描画( Device device, Point pt, float depth, Rectangle rc画像内の描画領域 )
		{
			this.t2D上下反転描画( device, pt.X, pt.Y, depth, rc画像内の描画領域 );
		}

        public static Vector3 t論理画面座標をワールド座標へ変換する(int x, int y)
        {
            return CTexture.t論理画面座標をワールド座標へ変換する(new Vector3((float)x, (float)y, 0f));
        }
        public static Vector3 t論理画面座標をワールド座標へ変換する(float x, float y)
        {
            return CTexture.t論理画面座標をワールド座標へ変換する(new Vector3(x, y, 0f));
        }
        public static Vector3 t論理画面座標をワールド座標へ変換する(Point pt論理画面座標)
        {
            return CTexture.t論理画面座標をワールド座標へ変換する(new Vector3(pt論理画面座標.X, pt論理画面座標.Y, 0.0f));
        }
        public static Vector3 t論理画面座標をワールド座標へ変換する(Vector2 v2論理画面座標)
        {
            return CTexture.t論理画面座標をワールド座標へ変換する(new Vector3(v2論理画面座標, 0f));
        }
        public static Vector3 t論理画面座標をワールド座標へ変換する(Vector3 v3論理画面座標)
        {
            return new Vector3(
                (v3論理画面座標.X - (CTexture.sz論理画面.Width / 2.0f)) * CTexture.f画面比率,
                (-(v3論理画面座標.Y - (CTexture.sz論理画面.Height / 2.0f)) * CTexture.f画面比率),
                v3論理画面座標.Z);
        }

		/// <summary>
		/// テクスチャを 3D 画像と見なして描画する。
		/// </summary>
		public void t3D描画( Device device, Matrix mat )
		{
			this.t3D描画( device, mat, this.rc全画像 );
		}
		public void t3D描画( Device device, Matrix mat, Rectangle rc画像内の描画領域 )
		{
			if( this.texture == null )
				return;

			float x = ( (float) rc画像内の描画領域.Width ) / 2f;
			float y = ( (float) rc画像内の描画領域.Height ) / 2f;
			float z = 0.0f;
			float f左U値 = ( (float) rc画像内の描画領域.Left ) / ( (float) this.szテクスチャサイズ.Width );
			float f右U値 = ( (float) rc画像内の描画領域.Right ) / ( (float) this.szテクスチャサイズ.Width );
			float f上V値 = ( (float) rc画像内の描画領域.Top ) / ( (float) this.szテクスチャサイズ.Height );
			float f下V値 = ( (float) rc画像内の描画領域.Bottom ) / ( (float) this.szテクスチャサイズ.Height );
			this.color4.Alpha = ( (float) this._透明度 ) / 255f;
			int color = this.color4.ToArgb();
			
			if( this.cvPositionColoredVertexies == null )
				this.cvPositionColoredVertexies = new PositionColoredTexturedVertex[ 4 ];

			// #27122 2012.1.13 from: 以下、マネージドオブジェクト（＝ガベージ）の量産を抑えるため、new は使わず、メンバに値を１つずつ直接上書きする。

			this.cvPositionColoredVertexies[ 0 ].Position.X = -x;
			this.cvPositionColoredVertexies[ 0 ].Position.Y = y;
			this.cvPositionColoredVertexies[ 0 ].Position.Z = z;
			this.cvPositionColoredVertexies[ 0 ].Color = color;
			this.cvPositionColoredVertexies[ 0 ].TextureCoordinates.X = f左U値;
			this.cvPositionColoredVertexies[ 0 ].TextureCoordinates.Y = f上V値;

			this.cvPositionColoredVertexies[ 1 ].Position.X = x;
			this.cvPositionColoredVertexies[ 1 ].Position.Y = y;
			this.cvPositionColoredVertexies[ 1 ].Position.Z = z;
			this.cvPositionColoredVertexies[ 1 ].Color = color;
			this.cvPositionColoredVertexies[ 1 ].TextureCoordinates.X = f右U値;
			this.cvPositionColoredVertexies[ 1 ].TextureCoordinates.Y = f上V値;

			this.cvPositionColoredVertexies[ 2 ].Position.X = -x;
			this.cvPositionColoredVertexies[ 2 ].Position.Y = -y;
			this.cvPositionColoredVertexies[ 2 ].Position.Z = z;
			this.cvPositionColoredVertexies[ 2 ].Color = color;
			this.cvPositionColoredVertexies[ 2 ].TextureCoordinates.X = f左U値;
			this.cvPositionColoredVertexies[ 2 ].TextureCoordinates.Y = f下V値;

			this.cvPositionColoredVertexies[ 3 ].Position.X = x;
			this.cvPositionColoredVertexies[ 3 ].Position.Y = -y;
			this.cvPositionColoredVertexies[ 3 ].Position.Z = z;
			this.cvPositionColoredVertexies[ 3 ].Color = color;
			this.cvPositionColoredVertexies[ 3 ].TextureCoordinates.X = f右U値;
			this.cvPositionColoredVertexies[ 3 ].TextureCoordinates.Y = f下V値;

			this.tレンダリングステートの設定( device );

			device.SetTransform( TransformState.World, mat );
			device.SetTexture( 0, this.texture );
			device.VertexFormat = PositionColoredTexturedVertex.Format;
			device.DrawUserPrimitives( PrimitiveType.TriangleStrip, 2, this.cvPositionColoredVertexies );
		}

        public void t3D左上基準描画( Device device, Matrix mat )
		{
			this.t3D左上基準描画( device, mat, this.rc全画像 );
		}
		/// <summary>
		/// ○覚書
		///   SlimDX.Matrix mat = SlimDX.Matrix.Identity;
		///   mat *= SlimDX.Matrix.Translation( x, y, z );
		/// 「mat =」ではなく「mat *=」であることを忘れないこと。
		/// </summary>
		public void t3D左上基準描画( Device device, Matrix mat, Rectangle rc画像内の描画領域 )
		{
			//とりあえず補正値などは無し。にしても使う機会少なさそうだなー・・・・
			if( this.texture == null )
				return;

			float x = 0.0f;
			float y = 0.0f;
			float z = 0.0f;
			float f左U値 = ( (float) rc画像内の描画領域.Left ) / ( (float) this.szテクスチャサイズ.Width );
			float f右U値 = ( (float) rc画像内の描画領域.Right ) / ( (float) this.szテクスチャサイズ.Width );
			float f上V値 = ( (float) rc画像内の描画領域.Top ) / ( (float) this.szテクスチャサイズ.Height );
			float f下V値 = ( (float) rc画像内の描画領域.Bottom ) / ( (float) this.szテクスチャサイズ.Height );
			this.color4.Alpha = ( (float) this._透明度 ) / 255f;
			int color = this.color4.ToArgb();
			
			if( this.cvPositionColoredVertexies == null )
				this.cvPositionColoredVertexies = new PositionColoredTexturedVertex[ 4 ];

			// #27122 2012.1.13 from: 以下、マネージドオブジェクト（＝ガベージ）の量産を抑えるため、new は使わず、メンバに値を１つずつ直接上書きする。

			this.cvPositionColoredVertexies[ 0 ].Position.X = -x;
			this.cvPositionColoredVertexies[ 0 ].Position.Y = y;
			this.cvPositionColoredVertexies[ 0 ].Position.Z = z;
			this.cvPositionColoredVertexies[ 0 ].Color = color;
			this.cvPositionColoredVertexies[ 0 ].TextureCoordinates.X = f左U値;
			this.cvPositionColoredVertexies[ 0 ].TextureCoordinates.Y = f上V値;

			this.cvPositionColoredVertexies[ 1 ].Position.X = x;
			this.cvPositionColoredVertexies[ 1 ].Position.Y = y;
			this.cvPositionColoredVertexies[ 1 ].Position.Z = z;
			this.cvPositionColoredVertexies[ 1 ].Color = color;
			this.cvPositionColoredVertexies[ 1 ].TextureCoordinates.X = f右U値;
			this.cvPositionColoredVertexies[ 1 ].TextureCoordinates.Y = f上V値;

			this.cvPositionColoredVertexies[ 2 ].Position.X = -x;
			this.cvPositionColoredVertexies[ 2 ].Position.Y = -y;
			this.cvPositionColoredVertexies[ 2 ].Position.Z = z;
			this.cvPositionColoredVertexies[ 2 ].Color = color;
			this.cvPositionColoredVertexies[ 2 ].TextureCoordinates.X = f左U値;
			this.cvPositionColoredVertexies[ 2 ].TextureCoordinates.Y = f下V値;

			this.cvPositionColoredVertexies[ 3 ].Position.X = x;
			this.cvPositionColoredVertexies[ 3 ].Position.Y = -y;
			this.cvPositionColoredVertexies[ 3 ].Position.Z = z;
			this.cvPositionColoredVertexies[ 3 ].Color = color;
			this.cvPositionColoredVertexies[ 3 ].TextureCoordinates.X = f右U値;
			this.cvPositionColoredVertexies[ 3 ].TextureCoordinates.Y = f下V値;

			this.tレンダリングステートの設定( device );

			device.SetTransform( TransformState.World, mat );
			device.SetTexture( 0, this.texture );
			device.VertexFormat = PositionColoredTexturedVertex.Format;
			device.DrawUserPrimitives( PrimitiveType.TriangleStrip, 2, this.cvPositionColoredVertexies );
		}

		#region [ IDisposable 実装 ]
		//-----------------
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected void Dispose(bool disposeManagedObjects)
		{
			if (this.bDispose完了済み)
				return;

			if (disposeManagedObjects)
			{
				// (A) Managed リソースの解放
				// テクスチャの破棄 (SharpDXのテクスチャは、SharpDX側で管理されるため、FDKからはmanagedリソースと見做す)
				if (this.texture != null)
				{
					this.texture.Dispose();
					this.texture = null;
					this.bSlimDXTextureDispose完了済み = true;
				}
			}

			// (B) Unamanaged リソースの解放


			this.bDispose完了済み = true;
		}
		~CTexture()
		{
			// ファイナライザの動作時にtextureのDisposeがされていない場合は、
			// CTextureのDispose漏れと見做して警告をログ出力する
			if (!this.bSlimDXTextureDispose完了済み)
			{
                Trace.TraceWarning("CTexture: Dispose漏れを検出しました。(Size=({0}, {1}), filename={2}, label={3})", sz画像サイズ.Width, sz画像サイズ.Height, filename, label );
			}
			this.Dispose(false);
		}
		//-----------------
		#endregion


		// その他

		#region [ private ]
		//-----------------
		private int _透明度;
        private bool bDispose完了済み, bSlimDXTextureDispose完了済み;
		private PositionColoredTexturedVertex[] cvPositionColoredVertexies;
        protected TransformedColoredTexturedVertex[] cvTransformedColoredVertexies = new TransformedColoredTexturedVertex[]
		{
			new TransformedColoredTexturedVertex(),
			new TransformedColoredTexturedVertex(),
			new TransformedColoredTexturedVertex(),
			new TransformedColoredTexturedVertex(),
		};
		private const Pool poolvar =												// 2011.4.25 yyagi
#if TEST_Direct3D9Ex
			Pool.Default;
#else
			Pool.Managed;
#endif
//		byte[] _txData;
		static object lockobj = new object();

		private void tレンダリングステートの設定( Device device )
		{
			if( this.b加算合成 )
			{
				device.SetRenderState( RenderState.AlphaBlendEnable, true );
				device.SetRenderState( RenderState.SourceBlend, SlimDX.Direct3D9.Blend.SourceAlpha );				// 5
				device.SetRenderState( RenderState.DestinationBlend, SlimDX.Direct3D9.Blend.One );					// 2
			}
			else
			{
				device.SetRenderState( RenderState.AlphaBlendEnable, true );
				device.SetRenderState( RenderState.SourceBlend, SlimDX.Direct3D9.Blend.SourceAlpha );				// 5
				device.SetRenderState( RenderState.DestinationBlend, SlimDX.Direct3D9.Blend.InverseSourceAlpha );	// 6
			}
		}
		private Size t指定されたサイズを超えない最適なテクスチャサイズを返す( Device device, Size sz指定サイズ )
		{
			bool b条件付きでサイズは２の累乗でなくてもOK = ( device.Capabilities.TextureCaps & TextureCaps.NonPow2Conditional ) != 0;
			bool bサイズは２の累乗でなければならない = ( device.Capabilities.TextureCaps & TextureCaps.Pow2 ) != 0;
			bool b正方形でなければならない = ( device.Capabilities.TextureCaps & TextureCaps.SquareOnly ) != 0;
			int n最大幅 = device.Capabilities.MaxTextureWidth;
			int n最大高 = device.Capabilities.MaxTextureHeight;
			var szサイズ = new Size( sz指定サイズ.Width, sz指定サイズ.Height );
			
			if( bサイズは２の累乗でなければならない && !b条件付きでサイズは２の累乗でなくてもOK )
			{
				// 幅を２の累乗にする
				int n = 1;
				do
				{
					n *= 2;
				}
				while( n <= sz指定サイズ.Width );
				sz指定サイズ.Width = n;

				// 高さを２の累乗にする
				n = 1;
				do
				{
					n *= 2;
				}
				while( n <= sz指定サイズ.Height );
				sz指定サイズ.Height = n;
			}

			if( sz指定サイズ.Width > n最大幅 )
				sz指定サイズ.Width = n最大幅;

			if( sz指定サイズ.Height > n最大高 )
				sz指定サイズ.Height = n最大高;

			if( b正方形でなければならない )
			{
				if( szサイズ.Width > szサイズ.Height )
				{
					szサイズ.Height = szサイズ.Width;
				}
				else if( szサイズ.Width < szサイズ.Height )
				{
					szサイズ.Width = szサイズ.Height;
				}
			}

			return szサイズ;
		}

		
		// 2012.3.21 さらなる new の省略作戦

		protected Rectangle rc全画像;								// テクスチャ作ったらあとは不変
		protected Color4 color4 = new Color4( 1f, 1f, 1f, 1f );	// アルファ以外は不変
		//-----------------
		#endregion
	}
}
