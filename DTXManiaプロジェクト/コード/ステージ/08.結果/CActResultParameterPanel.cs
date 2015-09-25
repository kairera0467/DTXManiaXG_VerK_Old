using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CActResultParameterPanel : CActivity
	{
		// コンストラクタ

		public CActResultParameterPanel()
		{
            this.tx文字 = new CTexture[3];
            ST文字位置[] st文字位置Array = new ST文字位置[11];
            ST文字位置 st文字位置 = new ST文字位置();
            st文字位置.ch = '0';
            st文字位置.pt = new Point(0, 0);
            st文字位置Array[0] = st文字位置;
            ST文字位置 st文字位置2 = new ST文字位置();
            st文字位置2.ch = '1';
            st文字位置2.pt = new Point(14, 0);
            st文字位置Array[1] = st文字位置2;
            ST文字位置 st文字位置3 = new ST文字位置();
            st文字位置3.ch = '2';
            st文字位置3.pt = new Point(0x1c, 0);
            st文字位置Array[2] = st文字位置3;
            ST文字位置 st文字位置4 = new ST文字位置();
            st文字位置4.ch = '3';
            st文字位置4.pt = new Point(0x2a, 0);
            st文字位置Array[3] = st文字位置4;
            ST文字位置 st文字位置5 = new ST文字位置();
            st文字位置5.ch = '4';
            st文字位置5.pt = new Point(0x38, 0);
            st文字位置Array[4] = st文字位置5;
            ST文字位置 st文字位置6 = new ST文字位置();
            st文字位置6.ch = '5';
            st文字位置6.pt = new Point(0, 0x12);
            st文字位置Array[5] = st文字位置6;
            ST文字位置 st文字位置7 = new ST文字位置();
            st文字位置7.ch = '6';
            st文字位置7.pt = new Point(14, 0x12);
            st文字位置Array[6] = st文字位置7;
            ST文字位置 st文字位置8 = new ST文字位置();
            st文字位置8.ch = '7';
            st文字位置8.pt = new Point(0x1c, 0x12);
            st文字位置Array[7] = st文字位置8;
            ST文字位置 st文字位置9 = new ST文字位置();
            st文字位置9.ch = '8';
            st文字位置9.pt = new Point(0x2a, 0x12);
            st文字位置Array[8] = st文字位置9;
            ST文字位置 st文字位置10 = new ST文字位置();
            st文字位置10.ch = '9';
            st文字位置10.pt = new Point(0x38, 0x12);
            st文字位置Array[9] = st文字位置10;
            ST文字位置 st文字位置11 = new ST文字位置();
            st文字位置11.ch = '.';
            st文字位置11.pt = new Point(70, 0x12);
            st文字位置Array[10] = st文字位置11;
            this.st大文字位置 = st文字位置Array;

            ST文字位置[] st文字位置Array2 = new ST文字位置[11];
            ST文字位置 st文字位置12 = new ST文字位置();
            st文字位置12.ch = '0';
            st文字位置12.pt = new Point(0, 0x24);
            st文字位置Array2[0] = st文字位置12;
            ST文字位置 st文字位置13 = new ST文字位置();
            st文字位置13.ch = '1';
            st文字位置13.pt = new Point(14, 0x24);
            st文字位置Array2[1] = st文字位置13;
            ST文字位置 st文字位置14 = new ST文字位置();
            st文字位置14.ch = '2';
            st文字位置14.pt = new Point(0x1c, 0x24);
            st文字位置Array2[2] = st文字位置14;
            ST文字位置 st文字位置15 = new ST文字位置();
            st文字位置15.ch = '3';
            st文字位置15.pt = new Point(0x2a, 0x24);
            st文字位置Array2[3] = st文字位置15;
            ST文字位置 st文字位置16 = new ST文字位置();
            st文字位置16.ch = '4';
            st文字位置16.pt = new Point(0x38, 0x24);
            st文字位置Array2[4] = st文字位置16;
            ST文字位置 st文字位置17 = new ST文字位置();
            st文字位置17.ch = '5';
            st文字位置17.pt = new Point(0, 0x36);
            st文字位置Array2[5] = st文字位置17;
            ST文字位置 st文字位置18 = new ST文字位置();
            st文字位置18.ch = '6';
            st文字位置18.pt = new Point(14, 0x36);
            st文字位置Array2[6] = st文字位置18;
            ST文字位置 st文字位置19 = new ST文字位置();
            st文字位置19.ch = '7';
            st文字位置19.pt = new Point(0x1c, 0x36);
            st文字位置Array2[7] = st文字位置19;
            ST文字位置 st文字位置20 = new ST文字位置();
            st文字位置20.ch = '8';
            st文字位置20.pt = new Point(0x2a, 0x36);
            st文字位置Array2[8] = st文字位置20;
            ST文字位置 st文字位置21 = new ST文字位置();
            st文字位置21.ch = '9';
            st文字位置21.pt = new Point(0x38, 0x36);
            st文字位置Array2[9] = st文字位置21;
            ST文字位置 st文字位置22 = new ST文字位置();
            st文字位置22.ch = '%';
            st文字位置22.pt = new Point(70, 0x36);
            st文字位置Array2[10] = st文字位置22;
            this.st小文字位置 = st文字位置Array2;

            ST文字位置[] st文字位置Array3 = new ST文字位置[12];
            ST文字位置 st文字位置23 = new ST文字位置();
            st文字位置23.ch = '0';
            st文字位置23.pt = new Point(0, 0);
            st文字位置Array3[0] = st文字位置23;
            ST文字位置 st文字位置24 = new ST文字位置();
            st文字位置24.ch = '1';
            st文字位置24.pt = new Point(0x12, 0);
            st文字位置Array3[1] = st文字位置24;
            ST文字位置 st文字位置25 = new ST文字位置();
            st文字位置25.ch = '2';
            st文字位置25.pt = new Point(0x24, 0);
            st文字位置Array3[2] = st文字位置25;
            ST文字位置 st文字位置26 = new ST文字位置();
            st文字位置26.ch = '3';
            st文字位置26.pt = new Point(0x36, 0);
            st文字位置Array3[3] = st文字位置26;
            ST文字位置 st文字位置27 = new ST文字位置();
            st文字位置27.ch = '4';
            st文字位置27.pt = new Point(0x48, 0);
            st文字位置Array3[4] = st文字位置27;
            ST文字位置 st文字位置28 = new ST文字位置();
            st文字位置28.ch = '5';
            st文字位置28.pt = new Point(0, 0x18);
            st文字位置Array3[5] = st文字位置28;
            ST文字位置 st文字位置29 = new ST文字位置();
            st文字位置29.ch = '6';
            st文字位置29.pt = new Point(0x12, 0x18);
            st文字位置Array3[6] = st文字位置29;
            ST文字位置 st文字位置30 = new ST文字位置();
            st文字位置30.ch = '7';
            st文字位置30.pt = new Point(0x24, 0x18);
            st文字位置Array3[7] = st文字位置30;
            ST文字位置 st文字位置31 = new ST文字位置();
            st文字位置31.ch = '8';
            st文字位置31.pt = new Point(0x36, 0x18);
            st文字位置Array3[8] = st文字位置31;
            ST文字位置 st文字位置32 = new ST文字位置();
            st文字位置32.ch = '9';
            st文字位置32.pt = new Point(0x48, 0x18);
            st文字位置Array3[9] = st文字位置32;
            ST文字位置 st文字位置33 = new ST文字位置();
            st文字位置33.ch = '.';
            st文字位置33.pt = new Point(90, 24);
            st文字位置Array3[10] = st文字位置33;
            ST文字位置 st文字位置34 = new ST文字位置();
            st文字位置34.ch = '%';
            st文字位置34.pt = new Point(90, 0);
            st文字位置Array3[11] = st文字位置34;
            this.st特大文字位置 = st文字位置Array3;
            base.b活性化してない = true;
		}


		// メソッド

		public void tアニメを完了させる()
		{
			this.ct表示用.n現在の値 = this.ct表示用.n終了値;
		}


		// CActivity 実装

		public override void On活性化()
		{
			this.n本体X = 453;
			this.n本体Y = 11;
			this.sdDTXで指定されたフルコンボ音 = null;
			this.bフルコンボ音再生済み = false;
			base.On活性化();
		}
		public override void On非活性化()
		{
			if( this.ct表示用 != null )
			{
				this.ct表示用 = null;
			}
			if( this.sdDTXで指定されたフルコンボ音 != null )
			{
				CDTXMania.Sound管理.tサウンドを破棄する( this.sdDTXで指定されたフルコンボ音 );
				this.sdDTXで指定されたフルコンボ音 = null;
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.txパネル本体 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_result panel.png" ) );
				this.tx文字[ 0 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_parameter numbers.png" ) );
				this.tx文字[ 1 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_parameter numbers_em.png" ) );
                this.tx文字[ 2 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_numbers_large.png" ) );
				this.txWhite = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Tile white 64x64.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txパネル本体 );
				CDTXMania.tテクスチャの解放( ref this.tx文字[ 0 ] );
				CDTXMania.tテクスチャの解放( ref this.tx文字[ 1 ] );
                CDTXMania.tテクスチャの解放( ref this.tx文字[ 2 ] );
				CDTXMania.tテクスチャの解放( ref this.txWhite );
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
				this.ct表示用 = new CCounter( 0, 999, 2, CDTXMania.Timer );
				base.b初めての進行描画 = false;
			}
			this.ct表示用.t進行();
			if( this.txパネル本体 != null )
			{
				this.txパネル本体.t2D描画( CDTXMania.app.Device, this.n本体X, this.n本体Y );
			}
			int num = this.ct表示用.n現在の値;
			Point[] pointArray = new Point[] { new Point( 0x68, 0x29 ), new Point( 0xc7, 0x29 ), new Point( 0x129, 0x29 ) };
			for( int i = 0; i < 3; i++ )
			{
				int x = this.n本体X + pointArray[ i ].X;
				int y = this.n本体Y + pointArray[ i ].Y;
				if( num >= 0 )
				{
					this.t大文字表示( x, y, CDTXMania.stage結果.st演奏記録[ i ].nPerfect数.ToString( "00000" ) );
				}
				if( num >= 100 )
				{
					this.t大文字表示( x, y + 0x16, CDTXMania.stage結果.st演奏記録[ i ].nGreat数.ToString( "00000" ) );
				}
				if( num >= 200 )
				{
					this.t大文字表示( x, y + 0x2c, CDTXMania.stage結果.st演奏記録[ i ].nGood数.ToString( "00000" ) );
				}
				if( num >= 300 )
				{
					this.t大文字表示( x, y + 0x42, CDTXMania.stage結果.st演奏記録[ i ].nPoor数.ToString( "00000" ) );
				}
				if( num >= 400 )
				{
					this.t大文字表示( x, y + 0x58, CDTXMania.stage結果.st演奏記録[ i ].nMiss数.ToString( "00000" ) );
				}
				if( num >= 0 )
				{
					this.t小文字表示( x + 0x30, y, string.Format( "{0,3:##0}%", CDTXMania.stage結果.fPerfect率[ i ] ) );
				}
				if( num >= 100 )
				{
					this.t小文字表示( x + 0x30, y + 0x16, string.Format( "{0,3:##0}%", CDTXMania.stage結果.fGreat率[ i ] ) );
				}
				if( num >= 200 )
				{
					this.t小文字表示( x + 0x30, y + 0x2c, string.Format( "{0,3:##0}%", CDTXMania.stage結果.fGood率[ i ] ) );
				}
				if( num >= 300 )
				{
					this.t小文字表示( x + 0x30, y + 0x42, string.Format( "{0,3:##0}%", CDTXMania.stage結果.fPoor率[ i ] ) );
				}
				if( num >= 400 )
				{
					this.t小文字表示( x + 0x30, y + 0x58, string.Format( "{0,3:##0}%", CDTXMania.stage結果.fMiss率[ i ] ) );
				}
				if( num >= 500 )
				{
					this.t大文字表示( x, y + 110, string.Format( "{0,9:########0}", CDTXMania.stage結果.st演奏記録[ i ].n最大コンボ数 ) );
				}
				if( num >= 600 )
				{
					this.t大文字表示( x, y + 0x84, CDTXMania.stage結果.st演奏記録[ i ].nスコア.ToString( "000000000" ), CDTXMania.stage結果.b新記録スコア[ i ] );
				}
				if( num >= 700 )
				{
					this.t大文字表示( x, y + 0x9a, string.Format( "{0,9:#####0.00}", CDTXMania.stage結果.st演奏記録[ i ].db演奏型スキル値 ), CDTXMania.stage結果.b新記録スキル[ i ] );
				}
				if( num >= 700 )
				{
					this.t小文字表示( x + 0x48, y + 0x9a, "%" );
				}
				if( num >= 800 )
				{
					this.t大文字表示( x, y + 0xb0, string.Format( "{0,9:########0}", CDTXMania.stage結果.n演奏回数[ i ] ) );
				}
			}
			if( this.ct表示用.n現在の値 < 900 )
			{
				int num5 = this.ct表示用.n現在の値 / 100;
				double num6 = 1.0 - ( ( (double) ( this.ct表示用.n現在の値 % 100 ) ) / 100.0 );
				int num7 = 0x157;
				int num8 = 0x6d + ( num5 * 0x16 );
				int num9 = 0x11c;
				int num10 = 0x10;
				if( this.txWhite != null )
				{
					this.txWhite.n透明度 = (int) ( 255.0 * num6 );
					Rectangle rectangle = new Rectangle( 0, 0, 0x40, 0 );
					while( num9 > 0 )
					{
						rectangle.Height = num10;
						if( num9 < 0x40 )
						{
							rectangle.Width = num9;
						}
						//this.txWhite.t2D描画( CDTXMania.app.Device, num7, num8, rectangle );
						num7 += 0x40;
						num9 -= 0x40;
					}
				}
			}
			if( this.ct表示用.n現在の値 >= 900 )
			{
				for( int j = 0; j < 3; j++ )
				{
					if( CDTXMania.stage結果.st演奏記録[ j ].bフルコンボである )
					{
						if( this.ct表示用.b終了値に達した )
						{
							if( !this.bフルコンボ音再生済み )
							{
								if( ( ( CDTXMania.DTX.SOUND_FULLCOMBO != null ) && ( CDTXMania.DTX.SOUND_FULLCOMBO.Length > 0 ) ) && File.Exists( CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.SOUND_FULLCOMBO ) )
								{
									try
									{
										if( this.sdDTXで指定されたフルコンボ音 != null )
										{
											CDTXMania.Sound管理.tサウンドを破棄する( this.sdDTXで指定されたフルコンボ音 );
											this.sdDTXで指定されたフルコンボ音 = null;
										}
										this.sdDTXで指定されたフルコンボ音 = CDTXMania.Sound管理.tサウンドを生成する( CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.SOUND_FULLCOMBO );
										if( this.sdDTXで指定されたフルコンボ音 != null )
										{
											this.sdDTXで指定されたフルコンボ音.t再生を開始する();
										}
									}
									catch
									{
									}
								}
								else
								{
									CDTXMania.Skin.soundフルコンボ音.t再生する();
								}
								this.bフルコンボ音再生済み = true;
							}
						}
					}
				}
			}
			if( !this.ct表示用.b終了値に達した )
			{
				return 0;
			}
			return 1;
		}
		

		// その他

		#region [ private ]
		//-----------------
		[StructLayout( LayoutKind.Sequential )]
		private struct ST文字位置
		{
			public char ch;
			public Point pt;
		}

		private bool bフルコンボ音再生済み;
        private bool b新記録音再生済み;
		private CCounter ct表示用;
		private int n本体X;
		private int n本体Y;
		private CSound sdDTXで指定されたフルコンボ音;
		private readonly ST文字位置[] st小文字位置;
		private readonly ST文字位置[] st大文字位置;
        private readonly ST文字位置[] st特大文字位置;
		private CTexture txWhite;
		private CTexture txパネル本体;
        private CTexture tx達成率ゲージ;
        private CTexture txDifficulty;
        private CTexture txHiSpeed;
        private CTexture txPart;
        private CTexture txRisky;
		private CTexture[] tx文字 = new CTexture[ 2 ];

        private void t小文字表示(int x, int y, string str)
        {
            this.t小文字表示(x, y, str, false);
        }
		private void t小文字表示( int x, int y, string str, bool b強調 )
		{
			foreach( char ch in str )
			{
				for( int i = 0; i < this.st小文字位置.Length; i++ )
				{
					if( this.st小文字位置[ i ].ch == ch )
					{
						Rectangle rectangle = new Rectangle( this.st小文字位置[ i ].pt.X, this.st小文字位置[ i ].pt.Y, 14, 0x12 );
						if( ch == '%' )
						{
							rectangle.Width -= 2;
							rectangle.Height -= 2;
						}
						if( this.tx文字[ b強調 ? 1 : 0 ] != null )
						{
							this.tx文字[ b強調 ? 1 : 0 ].t2D描画( CDTXMania.app.Device, x, y, rectangle );
						}
						break;
					}
				}
				x += 11;
			}
		}
		private void t大文字表示( int x, int y, string str )
		{
			this.t大文字表示( x, y, str, false );
		}
		private void t大文字表示( int x, int y, string str, bool b強調 )
		{
			foreach( char ch in str )
			{
				for( int i = 0; i < this.st大文字位置.Length; i++ )
				{
					if( this.st大文字位置[ i ].ch == ch )
					{
						Rectangle rectangle = new Rectangle( this.st大文字位置[ i ].pt.X, this.st大文字位置[ i ].pt.Y, 14, 0x12 );
						if( ch == '.' )
						{
							rectangle.Width -= 2;
							rectangle.Height -= 2;
						}
						if( this.tx文字[ b強調 ? 1 : 0 ] != null )
						{
							this.tx文字[ b強調 ? 1 : 0 ].t2D描画( CDTXMania.app.Device, x, y, rectangle );
						}
						break;
					}
				}
				x += 11;
			}
		}
        private void t特大文字表示( int x, int y, string str )
        {
            this.t特大文字表示( x, y, str, false );
        }
        private void t特大文字表示( int x, int y, string str, bool bExtraLarge )
        {
            foreach( char c in str )
            {
                for( int j = 0; j < this.st特大文字位置.Length; j++ )
                {
                    if( this.st特大文字位置[ j ].ch == c )
                    {
                        int num = 0;
                        int num2 = 0;
                        if( bExtraLarge )
                        {
                            if( j < 5 )
                            {
                                num = 6 * j;
                                num2 = 48;
                            }
                            else if( j < 11 )
                            {
                                num = 6 * ( j - 5 );
                                num2 = 56;
                            }
                            else
                            {
                                num = 24;
                                num2 = 48;
                            }
                        }
                        Rectangle rc画像内の描画領域 = new Rectangle( this.st特大文字位置[ j ].pt.X + num, this.st特大文字位置[ j ].pt.Y + num2, bExtraLarge ? 24 : 18, bExtraLarge ? 32 : 24 );
                        if( c == '.' )
                        {
                            rc画像内の描画領域.Width -= 2;
                            rc画像内の描画領域.Height -= 2;
                        }
                        if (this.tx文字[ 2 ] != null)
                        {
                            this.tx文字[ 2 ].t2D描画( CDTXMania.app.Device, x, y, rc画像内の描画領域 );
                        }
                        break;
                    }
                }
                if (c == '.')
                {
                    x += bExtraLarge ? 20 : 14;
                }
                else
                {
                    x += bExtraLarge ? 23 : 17;
                }
            }
        }
		//-----------------
		#endregion
	}
}
