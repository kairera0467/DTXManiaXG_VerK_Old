﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using SlimDX;
using SlimDX.Direct3D9;
using FDK;

namespace DTXMania
{
	internal class CActSelectPreimageパネル : CActivity
	{
		// メソッド

		public CActSelectPreimageパネル()
		{
			base.b活性化してない = true;
		}
		public void t選択曲が変更された()
		{
			this.ct遅延表示 = new CCounter( -CDTXMania.ConfigIni.n曲が選択されてからプレビュー画像が表示開始されるまでのウェイトms, 100, 1, CDTXMania.Timer );
			this.b新しいプレビューファイルを読み込んだ = false;
		}

		public bool bIsPlayingPremovie		// #27060
		{
			get
			{
				return false;
			}
		}

		// CActivity 実装

		public override void On活性化()
		{
			this.n本体X = 8;
			this.n本体Y = 57;
            for (int i = 0; i < 13; i++)
            {
                this.r表示するプレビュー画像[i] = this.txプレビュー画像がないときの画像;
                this.str現在のファイル名[i] = "";
            }
			this.b新しいプレビューファイルを読み込んだ = false;
			base.On活性化();
		}
		public override void On非活性化()
		{
			this.ct登場アニメ用 = null;
			this.ct遅延表示 = null;
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.txパネル本体 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_preimage panel.png" ), false );
				this.txセンサ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_sensor.png" ), false );
				this.txセンサ光 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_sensor light.png" ), false );
				this.txプレビュー画像 = null;
                for (int i = 0; i < 13; i++)
                    this.txサムネイル画像[i] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\5_preimage default.png"), false);
				this.txプレビュー画像がないときの画像 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_preimage default.png" ), false );
				this.tプレビュー画像・動画の変更();
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txパネル本体 );
				CDTXMania.tテクスチャの解放( ref this.txセンサ );
				CDTXMania.tテクスチャの解放( ref this.txセンサ光 );
				CDTXMania.tテクスチャの解放( ref this.txプレビュー画像 );
                for (int i = 0; i < 13; i++)
                    CDTXMania.tテクスチャの解放( ref this.txサムネイル画像[i] );
                CDTXMania.tテクスチャの解放( ref this.txプレビュー画像がないときの画像 );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
				if( base.b初めての進行描画 )
				{
					this.ct登場アニメ用 = new CCounter( 0, 100, 5, CDTXMania.Timer );
					this.ctセンサ光 = new CCounter( 0, 100, 30, CDTXMania.Timer );
					this.ctセンサ光.n現在の値 = 70;
					base.b初めての進行描画 = false;
				}
				this.ct登場アニメ用.t進行();
				this.ctセンサ光.t進行Loop();
				if( ( !CDTXMania.stage選曲.bスクロール中 && ( this.ct遅延表示 != null ) ) && this.ct遅延表示.b進行中 )
				{
					this.ct遅延表示.t進行();
					if( this.ct遅延表示.b終了値に達した )
					{
						this.ct遅延表示.t停止();
					}
					else if( ( this.ct遅延表示.n現在の値 >= 0 ) && this.b新しいプレビューファイルをまだ読み込んでいない )
					{
						this.tプレビュー画像・動画の変更();
						CDTXMania.Timer.t更新();
						this.ct遅延表示.n現在の経過時間ms = CDTXMania.Timer.n現在時刻;
						this.b新しいプレビューファイルを読み込んだ = true;
					}
				}
				this.t描画処理・パネル本体();
				this.t描画処理・ジャンル文字列();
				//this.t描画処理・プレビュー画像();
                this.t描画処理・サムネイル画像();
				this.t描画処理・センサ光();
				this.t描画処理・センサ本体();
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		private CCounter ctセンサ光;
		private CCounter ct遅延表示;
		private CCounter ct登場アニメ用;
		private int n本体X;
		private int n本体Y;
		private readonly Rectangle rcセンサ光 = new Rectangle( 0, 0x120, 0x80, 0x60 );
		private readonly Rectangle rcセンサ本体下半分 = new Rectangle( 0x80, 0, 0x80, 0xc0 );
		private readonly Rectangle rcセンサ本体上半分 = new Rectangle( 0, 0, 0x80, 0xc0 );
		private CTexture[] r表示するプレビュー画像 = new CTexture[13];
		private string[] str現在のファイル名 = new string[13];
		private CTexture txセンサ;
		private CTexture txセンサ光;
		private CTexture txパネル本体;
		private CTexture txプレビュー画像;
        private CTexture[] txサムネイル画像 = new CTexture[13];
		private CTexture txプレビュー画像がないときの画像;
        private struct ST中心点
        {
            public float x;
            public float y;
            public float z;
            public float rotY;
        }
        private ST中心点[] stマトリクス座標 = new ST中心点[13]{
             new ST中心点() { x = -533.8936f, y = 50f, z = -289.5575f, rotY = -0.9279888f},
             new ST中心点() { x = -533.8936f, y = 50f, z = -289.5575f, rotY = -0.9279888f},
             new ST中心点() { x = -423.8936f, y = 50f, z = -169.5575f, rotY = -0.6579891f},
             new ST中心点() { x = -297.5025f, y = 50f, z = -74.37564f, rotY = -0.4808382f},
             new ST中心点() { x = -153.9001f, y = 50f, z = -20.52002f, rotY = -0.2605f },
             new ST中心点() { x = 0.00002622683f, y = 50f, z = 0f, rotY = 0f },
             new ST中心点() { x = 153.9002f, y = 50f, z = -20.52002f, rotY = 0.2605f },
             new ST中心点() { x = 297.5025f, y = 50f, z = -74.37564f, rotY = 0.4808382f },
             new ST中心点() { x = 423.8936f, y = 50f, z = -169.5575f, rotY = 0.6579891f },
             new ST中心点() { x = 533.8936f, y = 50f, z = -289.5575f, rotY = 0.9279888f },
             new ST中心点() { x = 533.8936f, y = 50f, z = -289.5575f, rotY = 0.9279888f },
             new ST中心点() { x = 533.8936f, y = 50f, z = -289.5575f, rotY = 0.9279888f },
             new ST中心点() { x = 533.8936f, y = 50f, z = -289.5575f, rotY = 0.9279888f },
        };
		private bool b新しいプレビューファイルを読み込んだ;
		private bool b新しいプレビューファイルをまだ読み込んでいない
		{
			get
			{
				return !this.b新しいプレビューファイルを読み込んだ;
			}
			set
			{
				this.b新しいプレビューファイルを読み込んだ = !value;
			}
		}

		private unsafe void tサーフェイスをクリアする( Surface sf )
		{
			DataRectangle rectangle = sf.LockRectangle( LockFlags.None );
			DataStream data = rectangle.Data;
			switch( ( rectangle.Pitch / sf.Description.Width ) )
			{
				case 4:
					{
						uint* numPtr = (uint*) data.DataPointer.ToPointer();
						for( int i = 0; i < sf.Description.Height; i++ )
						{
							for( int j = 0; j < sf.Description.Width; j++ )
							{
								( numPtr + ( i * sf.Description.Width ) )[ j ] = 0;
							}
						}
						break;
					}
				case 2:
					{
						ushort* numPtr2 = (ushort*) data.DataPointer.ToPointer();
						for( int k = 0; k < sf.Description.Height; k++ )
						{
							for( int m = 0; m < sf.Description.Width; m++ )
							{
								( numPtr2 + ( k * sf.Description.Width ) )[ m ] = 0;
							}
						}
						break;
					}
			}
			sf.UnlockRectangle();
		}
        public void tプレビュー画像・動画の変更()
        {
            //if (!CDTXMania.ConfigIni.bストイックモード)
            {
                for (int i = 0; i < 13; i++)
                {
                    //ここでは変更が効くが、どうやらstバー情報が更新されないようだ。なんてこった・・・・・
                    this.t指定された曲からプレビュー画像を構築する(CDTXMania.stage選曲.act曲リスト.stバー情報[i].cScore, i);
                    //{
                        //if (i == 13)
                        //    return;
                        //else
                            //continue;
                    //}
                    //else
                    //{
                    //    this.r表示するプレビュー画像[i] = this.txプレビュー画像がないときの画像;
                    //    this.str現在のファイル名[i] = "";
                        //if (i == 13)
                        //    return;
                        //else
                    //        continue;
                    //}
                }
                return;
            }
        }
		private bool tプレビュー画像の指定があれば構築する()
		{
			Cスコア cスコア = CDTXMania.stage選曲.r現在選択中のスコア;
			if( ( cスコア == null ) || string.IsNullOrEmpty( cスコア.譜面情報.Preimage ) )
			{
				return false;
			}
			string str = cスコア.ファイル情報.フォルダの絶対パス + cスコア.譜面情報.Preimage;
			if( !str.Equals( this.str現在のファイル名[5] ) )
			{
				CDTXMania.tテクスチャの解放( ref this.txプレビュー画像 );
				this.str現在のファイル名[5] = str;
				if( !File.Exists( this.str現在のファイル名[5] ) )
				{
					Trace.TraceWarning( "ファイルが存在しません。({0})", new object[] { this.str現在のファイル名[5] } );
					return false;
				}
                for (int i = 0; i < 13; i++)
                {
                    this.txプレビュー画像 = CDTXMania.tテクスチャの生成(this.str現在のファイル名[5], false);
                    if (this.txプレビュー画像 != null)
                    {
                        this.r表示するプレビュー画像[ i ] = this.txプレビュー画像;
                    }
                    else
                    {
                        this.r表示するプレビュー画像[ i ] = this.txプレビュー画像がないときの画像;
                    }
                }
			}
			return true;
		}
        public bool t指定された曲からプレビュー画像を構築する(Cスコア cスコア, int nバー番号)
        {
            //Cスコア cスコア = CDTXMania.stage選曲.r現在選択中のスコア;
            //cスコア = CDTXMania.stage選曲.r現在選択中のスコア;

            #region[選択中のやつ]
            /*
            if (nバー番号 == 5)
            {
                cスコア = CDTXMania.stage選曲.r現在選択中のスコア;
                if ((cスコア == null) || string.IsNullOrEmpty(cスコア.譜面情報.Preimage))
                {
                    return false;
                }
                string str = cスコア.ファイル情報.フォルダの絶対パス + cスコア.譜面情報.Preimage;
                if (!str.Equals(this.str現在のファイル名[ 5 ]))
                {
                    Trace.TraceInformation("-----------------------------------------");
                    CDTXMania.tテクスチャの解放(ref this.txサムネイル画像[ 5 ]);
                    this.str現在のファイル名[nバー番号] = str;
                    if (!File.Exists(this.str現在のファイル名[ 5 ]))
                    {
                        Trace.TraceWarning("ファイルが存在しません。({0})", new object[] { this.str現在のファイル名[ 5 ] });
                        return false;
                    }

                    if (this.txサムネイル画像[ 5 ] == null)
                    {
                        this.txサムネイル画像[ 5 ] = CDTXMania.tテクスチャの生成(this.str現在のファイル名[ 5 ], false);
                        if (this.txサムネイル画像[ 5 ] != null)
                        {
                            this.r表示するプレビュー画像[ 5 ] = this.txサムネイル画像[nバー番号];
                            Trace.TraceInformation("テクスチャを生成しました。({0} {1})", this.str現在のファイル名[ 5 ], nバー番号);
                        }
                        else
                        {
                            this.r表示するプレビュー画像[ 5 ] = this.txプレビュー画像がないときの画像;
                            Trace.TraceWarning("テクスチャがありませんでした。デフォルト指定のものを使います。");
                        }
                    }
                }
            }
            */
            #endregion
            //else
            {
                if ((cスコア == null) || string.IsNullOrEmpty(cスコア.譜面情報.Preimage))
                {
                    this.r表示するプレビュー画像[nバー番号] = this.txプレビュー画像がないときの画像;
                    return false;
                }
                string str = cスコア.ファイル情報.フォルダの絶対パス + cスコア.譜面情報.Preimage;
                if (!str.Equals(this.str現在のファイル名[nバー番号]))
                {
                    //現状バー情報が変わらないため、この分岐文に突入できていない。
                    Trace.TraceInformation("-----------------------------------------");
                    CDTXMania.tテクスチャの解放(ref this.txサムネイル画像[nバー番号]);
                    this.str現在のファイル名[nバー番号] = str;
                    if (!File.Exists(this.str現在のファイル名[nバー番号]))
                    {
                        Trace.TraceWarning("ファイルが存在しません。({0})", new object[] { this.str現在のファイル名[nバー番号] });
                        return false;
                    }

                    if (this.txサムネイル画像[nバー番号] == null)
                    {
                        this.txサムネイル画像[nバー番号] = CDTXMania.tテクスチャの生成(this.str現在のファイル名[nバー番号], false);
                        if (this.txサムネイル画像[nバー番号] != null)
                        {
                            this.r表示するプレビュー画像[nバー番号] = this.txサムネイル画像[nバー番号];
                            Trace.TraceInformation("テクスチャを生成しました。({0} {1})", this.str現在のファイル名[nバー番号], nバー番号);
                        }
                        else
                        {
                            this.r表示するプレビュー画像[nバー番号] = this.txプレビュー画像がないときの画像;
                            Trace.TraceWarning("テクスチャがありませんでした。デフォルト指定のものを使います。");
                        }
                    }
                }
            }
            return true;
        }

		private bool t背景画像があればその一部からプレビュー画像を構築する()
		{
			Cスコア cスコア = CDTXMania.stage選曲.r現在選択中のスコア;
			if( ( cスコア == null ) || string.IsNullOrEmpty( cスコア.譜面情報.Backgound ) )
			{
				return false;
			}
			string path = cスコア.ファイル情報.フォルダの絶対パス + cスコア.譜面情報.Backgound;
			if( !path.Equals( this.str現在のファイル名 ) )
			{
				if( !File.Exists( path ) )
				{
					Trace.TraceWarning( "ファイルが存在しません。({0})", new object[] { path } );
					return false;
				}
				CDTXMania.tテクスチャの解放( ref this.txプレビュー画像 );
				this.str現在のファイル名[5] = path;
				Bitmap image = null;
				Bitmap bitmap2 = null;
				Bitmap bitmap3 = null;
				try
				{
					image = new Bitmap( this.str現在のファイル名[5] );
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
					graphics.DrawImage( bitmap2, 5, 5, new Rectangle( 0x157, 0x6d, 204, 269 ), GraphicsUnit.Pixel );
					graphics.Dispose();
					this.txプレビュー画像 = new CTexture( CDTXMania.app.Device, bitmap3, CDTXMania.TextureFormat );
                    for (int i = 0; i < 13; i++)
					    this.r表示するプレビュー画像[ i ] = this.txプレビュー画像;
				}
				catch
				{
					Trace.TraceError( "背景画像の読み込みに失敗しました。({0})", new object[] { this.str現在のファイル名 } );
                    for (int i = 0; i < 13; i++)
					this.r表示するプレビュー画像[ i ] = this.txプレビュー画像がないときの画像;
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
			}
			return true;
		}
		private void t描画処理・ジャンル文字列()
		{
			C曲リストノード c曲リストノード = CDTXMania.stage選曲.r現在選択中の曲;
			Cスコア cスコア = CDTXMania.stage選曲.r現在選択中のスコア;
			if( ( c曲リストノード != null ) && ( cスコア != null ) )
			{
				string str = "";
				switch( c曲リストノード.eノード種別 )
				{
					case C曲リストノード.Eノード種別.SCORE:
						if( ( c曲リストノード.strジャンル == null ) || ( c曲リストノード.strジャンル.Length <= 0 ) )
						{
							if( ( cスコア.譜面情報.ジャンル != null ) && ( cスコア.譜面情報.ジャンル.Length > 0 ) )
							{
								str = cスコア.譜面情報.ジャンル;
							}
							else
							{
								switch( cスコア.譜面情報.曲種別 )
								{
									case CDTX.E種別.DTX:
										str = "DTX";
										break;

									case CDTX.E種別.GDA:
										str = "GDA";
										break;

									case CDTX.E種別.G2D:
										str = "G2D";
										break;

									case CDTX.E種別.BMS:
										str = "BMS";
										break;

									case CDTX.E種別.BME:
										str = "BME";
										break;
								}
								str = "Unknown";
							}
							break;
						}
						str = c曲リストノード.strジャンル;
						break;

					case C曲リストノード.Eノード種別.SCORE_MIDI:
						str = "MIDI";
						break;

					case C曲リストノード.Eノード種別.BOX:
						str = "MusicBox";
						break;

					case C曲リストノード.Eノード種別.BACKBOX:
						str = "BackBox";
						break;

					case C曲リストノード.Eノード種別.RANDOM:
						str = "Random";
						break;

					default:
						str = "Unknown";
						break;
				}
				CDTXMania.act文字コンソール.tPrint( this.n本体X + 0x12, this.n本体Y - 1, C文字コンソール.Eフォント種別.赤細, str );
			}
		}
		private void t描画処理・センサ光()
		{
			int num = this.ctセンサ光.n現在の値;
			if( num < 12 )
			{
				int x = this.n本体X + 0x198;
				int y = this.n本体Y + 0xb9;
				if( this.txセンサ光 != null )
				{
					this.txセンサ光.vc拡大縮小倍率 = new Vector3( 1f, 1f, 1f );
					this.txセンサ光.n透明度 = 0xff;
					this.txセンサ光.t2D描画( CDTXMania.app.Device, x, y, new Rectangle( ( num % 4 ) * 0x80, ( num / 4 ) * 0x60, 0x80, 0x60 ) );
				}
			}
			else if( num < 0x18 )
			{
				int num4 = num - 11;
				double num5 = ( (double) num4 ) / 11.0;
				double num6 = 1.0 + ( num5 * 0.5 );
				int num7 = (int) ( 128.0 * num6 );
				int num8 = (int) ( 96.0 * num6 );
				int num9 = ( ( this.n本体X + 0x198 ) + 0x40 ) - ( num7 / 2 );
				int num10 = ( ( this.n本体Y + 0xb9 ) + 0x30 ) - ( num8 / 2 );
				if( this.txセンサ光 != null )
				{
					this.txセンサ光.vc拡大縮小倍率 = new Vector3( (float) num6, (float) num6, 1f );
					this.txセンサ光.n透明度 = (int) ( 255.0 * ( 1.0 - num5 ) );
					this.txセンサ光.t2D描画( CDTXMania.app.Device, num9, num10, this.rcセンサ光 );
				}
			}
		}
		private void t描画処理・センサ本体()
		{
			int x = this.n本体X + 410;
			int y = this.n本体Y - 6;
			if( this.txセンサ != null )
			{
				this.txセンサ.t2D描画( CDTXMania.app.Device, x, y, this.rcセンサ本体上半分 );
				y += 0xc0;
				this.txセンサ.t2D描画( CDTXMania.app.Device, x, y, this.rcセンサ本体下半分 );
			}
		}
		private void t描画処理・パネル本体()
		{
			if( this.ct登場アニメ用.b終了値に達した || ( this.txパネル本体 != null ) )
			{
				this.n本体X = 0x10;
				this.n本体Y = 0x56;
			}
			else
			{
				double num = ( (double) this.ct登場アニメ用.n現在の値 ) / 100.0;
				double num2 = Math.Cos( ( 1.5 + ( 0.5 * num ) ) * Math.PI );
				this.n本体X = 0x10;
				this.n本体Y = 0x56 - ( (int) ( this.txパネル本体.sz画像サイズ.Height * ( 1.0 - ( num2 * num2 ) ) ) );
			}
			if( this.txパネル本体 != null )
			{
				this.txパネル本体.t2D描画( CDTXMania.app.Device, this.n本体X, this.n本体Y );
			}
		}
		private unsafe void t描画処理・プレビュー画像()
		{
			if( !CDTXMania.stage選曲.bスクロール中 && ( ( ( this.ct遅延表示 != null ) && ( this.ct遅延表示.n現在の値 > 0 ) ) && !this.b新しいプレビューファイルをまだ読み込んでいない ) )
			{
                for (int i = 0; i < 13; i++)
                {
                    int x = this.n本体X + 0x24;
                    int y = this.n本体Y + 0x18;
                    float num3 = ((float)this.ct遅延表示.n現在の値) / 100f;
                    float num4 = 0.9f + (0.1f * num3);
                    if (this.r表示するプレビュー画像[i] != null)
                    {
                        int width = this.r表示するプレビュー画像[i].sz画像サイズ.Width;
                        int height = this.r表示するプレビュー画像[i].sz画像サイズ.Height;
                        if (width > 0x198)
                        {
                            width = 0x198;
                        }
                        if (height > 0x194)
                        {
                            height = 0x194;
                        }
                        x += (0x198 - ((int)(width * num4))) / 2;
                        y += (0x194 - ((int)(height * num4))) / 2;
                        this.r表示するプレビュー画像[i].n透明度 = (int)(255f * num3);
                        this.r表示するプレビュー画像[i].vc拡大縮小倍率.X = num4;
                        this.r表示するプレビュー画像[i].vc拡大縮小倍率.Y = num4;
                        //this.r表示するプレビュー画像.t2D描画( CDTXMania.app.Device, x, y, new Rectangle( 0, 0, width, height ) );
                    }
                }
			}
		}
        public unsafe void t描画処理・サムネイル画像()
        {
            #region[ 3D描画 ]
            for (int n = 0; n < 13; n++ )
            {
                if (this.r表示するプレビュー画像[n] != null)
                {
                    var mat = SlimDX.Matrix.Identity;
                    mat *= SlimDX.Matrix.Scaling(0.3f * CTexture.f画面比率, 0.3f * CTexture.f画面比率, 1.0f);
                    mat *= SlimDX.Matrix.RotationY(this.stマトリクス座標[n].rotY + (this.stマトリクス座標[n].rotY - this.stマトリクス座標[n].rotY) * 0.4f);
                    mat *= SlimDX.Matrix.Translation(
                        (this.stマトリクス座標[n].x + (int)((this.stマトリクス座標[n].x - this.stマトリクス座標[n].x) * 1.0f)) * CTexture.f画面比率,
                        (this.stマトリクス座標[n].y + (int)((this.stマトリクス座標[n].y - this.stマトリクス座標[n].y) * 1.0f)) * CTexture.f画面比率,
                        (this.stマトリクス座標[n].z + (int)((this.stマトリクス座標[n].z - this.stマトリクス座標[n].z) * 1.0f)) * CTexture.f画面比率);

                    this.r表示するプレビュー画像[n].t3D描画(CDTXMania.app.Device, mat);
                }
            }
            #endregion
        }
		//-----------------
		#endregion
	}
}
