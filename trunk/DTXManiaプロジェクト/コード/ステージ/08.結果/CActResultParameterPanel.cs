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

            #region [ 本体位置 ]

            int n上X = 453;
            int n上Y = 11;

            int n下X = 106;
            int n下Y = 430;

            this.n本体1X = 2000;
            this.n本体1Y = 2000;

            this.n本体2X = 2000;
            this.n本体2Y = 2000;

            if (CDTXMania.ConfigIni.bDrums有効)
            {
                this.n本体1X = n上X;
                this.n本体1Y = n上Y;
            }
            else if (CDTXMania.ConfigIni.bGuitar有効)
            {
                if (CDTXMania.DTX.bチップがある.Guitar)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体1X = n下X;
                        this.n本体1Y = n下Y;
                    }
                    else
                    {
                        this.n本体1X = n上X;
                        this.n本体1Y = n上Y;
                    }
                }

                if (CDTXMania.DTX.bチップがある.Bass)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体2X = n上X;
                        this.n本体2Y = n上Y;
                    }
                    else
                    {
                        this.n本体2X = n下X;
                        this.n本体2Y = n下Y;
                    }
                }

            }
            #endregion

            this.b新記録音再生済み = false;
			base.On活性化();
		}
		public override void On非活性化()
		{
			if( this.ct表示用 != null )
			{
				this.ct表示用 = null;
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.tx文字[0] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_numbers.png"));
                this.tx文字[1] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_numbers_em.png"));
                this.tx文字[2] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_numbers_large.png"));
                this.txNewRecord = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_New Record.png"));
                this.txWhite = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\Tile white 64x64.png"));
                this.tx達成率ゲージ = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_gauge.jpg"));
                this.txDifficulty = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_Difficulty.png"));
                this.txPart = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_Part.png"));
                this.txリザルトパネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_result panel.png"));

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
                CDTXMania.tテクスチャの解放( ref this.txNewRecord );
				CDTXMania.tテクスチャの解放( ref this.txWhite );
                CDTXMania.tテクスチャの解放( ref this.tx達成率ゲージ );
                CDTXMania.tテクスチャの解放(ref this.txDifficulty);
                CDTXMania.tテクスチャの解放(ref this.txPart);
                CDTXMania.tテクスチャの解放(ref this.txリザルトパネル);
                base.OnManagedリソースの解放();
			}
		}
        public override int On進行描画()
        {
                if (base.b活性化してない)
                {
                    return 0;
                }
                if (base.b初めての進行描画)
                {
                    this.ct表示用 = new CCounter(0, 1000, 3, CDTXMania.Timer);
                    base.b初めての進行描画 = false;
                }

                this.ct表示用.t進行();
                double dbドラムメーター = ((3.5 * ((CDTXMania.stage結果.st演奏記録.Drums.b全AUTOじゃない ? CDTXMania.stage結果.st演奏記録.Drums.db演奏型スキル値 : 100.0))) / 500.0) * (this.ct表示用.n現在の値 > 500 ? 500 : this.ct表示用.n現在の値);
                double dbギターメーター = ((3.5 * ((CDTXMania.stage結果.st演奏記録.Guitar.b全AUTOじゃない ? CDTXMania.stage結果.st演奏記録.Guitar.db演奏型スキル値 : 100.0))) / 500.0) * (this.ct表示用.n現在の値 > 500 ? 500 : this.ct表示用.n現在の値);
                double dbベースメーター = ((3.5 * ((CDTXMania.stage結果.st演奏記録.Bass.b全AUTOじゃない ? CDTXMania.stage結果.st演奏記録.Bass.db演奏型スキル値 : 100.0))) / 500.0) * (this.ct表示用.n現在の値 > 500 ? 500 : this.ct表示用.n現在の値);

                int num = this.ct表示用.n現在の値;

                if ((CDTXMania.Input管理.Keyboard.bキーが押されている(0x3c)))
                {
                    //F7
                    //CDTXMania.stage演奏ドラム画面.actGauge.db現在のゲージ値.Drums = 1.0;
                    //CDTXMania.stage演奏ドラム画面.actAVI.LivePoint = 300.0;
                    //CDTXMania.stage演奏ドラム画面.actGraph.dbグラフ値現在_渡 = 100.0;
                    //CDTXMania.ConfigIni.nヒット範囲ms.Perfect = 1000;
                    //this.ct表示用.n現在の値 = 0;
                    //CDTXMania.stage結果.st演奏記録.Drums.db演奏型スキル値 = 80.0;
                }


                if ( n本体1X != 2000 )
                {
                    if ( this.txリザルトパネル != null )
                        this.txリザルトパネル.t2D描画(CDTXMania.app.Device, n本体1X, n本体1Y);

                    if ( txDifficulty != null )
                        this.txDifficulty.t2D描画(CDTXMania.app.Device, this.n本体1X + 487, this.n本体1Y + 8, new Rectangle(0, CDTXMania.nSongDifficulty * 20, 140, 20));

                    if (this.txPart != null)
                    {
                        if (CDTXMania.ConfigIni.bDrums有効)
                            this.txPart.t2D描画(CDTXMania.app.Device, this.n本体1X + 347, this.n本体1Y + 8, new Rectangle(0, 0, 140, 20));
                        else
                            this.txPart.t2D描画(CDTXMania.app.Device, this.n本体1X + 347, this.n本体1Y + 8, new Rectangle(0, 20, 140, 20));
                    }
                }

                if ( n本体2X != 2000 )
                {
                    if (this.txリザルトパネル != null)
                        this.txリザルトパネル.t2D描画(CDTXMania.app.Device, n本体2X, n本体2Y);

                    if (txDifficulty != null)
                        this.txDifficulty.t2D描画(CDTXMania.app.Device, this.n本体2X + 487, this.n本体2Y + 8, new Rectangle(0, CDTXMania.nSongDifficulty * 20, 140, 20));

                    if ( this.txPart != null )
                        this.txPart.t2D描画(CDTXMania.app.Device, this.n本体2X + 347, this.n本体2Y + 8, new Rectangle(0, 40, 140, 20));
                }

                
                {
                    int x1 = this.n本体1X + 507;
                    int y1 = this.n本体1Y + 35;
                    int x2 = this.n本体2X + 507;
                    int y2 = this.n本体2Y + 35;

                    if (CDTXMania.ConfigIni.bDrums有効)
                    {
                        if ( CDTXMania.stage結果.st演奏記録.Drums.nPerfect数 == CDTXMania.stage結果.st演奏記録.Drums.n全チップ数 )
                        {
                            this.txNewRecord.t2D描画(CDTXMania.app.Device, this.n本体1X + 79, this.n本体1Y + 29, new Rectangle(0, 12, 72, 26));
                            this.t特大文字表示(x1 - 248, y1 - 3, string.Format("{0,6:##0.00}", CDTXMania.stage結果.st演奏記録.Drums.dbゲーム型スキル値));
                        }
                        else
                        {
                            this.t特大文字表示(x1 - 438, y1 - 4, string.Format("{0,-6:##0.00%}", CDTXMania.stage結果.st演奏記録.Drums.db演奏型スキル値 / 100.0));
                            this.t特大文字表示(x1 - 248, y1 - 3, string.Format("{0,6:##0.00}", CDTXMania.stage結果.st演奏記録.Drums.dbゲーム型スキル値));
                        }
                    }
                    else if (CDTXMania.ConfigIni.bGuitar有効)
                    {

                        if (CDTXMania.stage結果.st演奏記録.Guitar.nPerfect数 == CDTXMania.stage結果.st演奏記録.Guitar.n全チップ数)
                        {
                            this.txNewRecord.t2D描画(CDTXMania.app.Device, this.n本体1X + 79, this.n本体1Y + 29, new Rectangle(0, 12, 72, 26));
                            this.t特大文字表示(x1 - 248, y1 - 3, string.Format("{0,6:##0.00}", CDTXMania.stage結果.st演奏記録.Guitar.dbゲーム型スキル値));
                        }
                        else
                        {
                            this.t特大文字表示(x1 - 438, y1 - 4, string.Format("{0,-6:##0.00%}", CDTXMania.stage結果.st演奏記録.Guitar.db演奏型スキル値 / 100.0));
                            this.t特大文字表示(x1 - 248, y1 - 3, string.Format("{0,6:##0.00}", CDTXMania.stage結果.st演奏記録.Guitar.dbゲーム型スキル値));
                        }

                        if (CDTXMania.stage結果.st演奏記録.Bass.nPerfect数 == CDTXMania.stage結果.st演奏記録.Bass.n全チップ数)
                        {
                            this.txNewRecord.t2D描画(CDTXMania.app.Device, this.n本体2X + 79, this.n本体2Y + 29, new Rectangle(0, 12, 72, 26));
                            this.t特大文字表示(x2 - 248, y2 - 3, string.Format("{0,6:##0.00}", CDTXMania.stage結果.st演奏記録.Bass.dbゲーム型スキル値));
                        }
                        else
                        {
                            this.t特大文字表示(x2 - 438, y2 - 4, string.Format("{0,-6:##0.00%}", CDTXMania.stage結果.st演奏記録.Bass.db演奏型スキル値 / 100.0));
                            this.t特大文字表示(x2 - 248, y2 - 3, string.Format("{0,6:##0.00}", CDTXMania.stage結果.st演奏記録.Bass.dbゲーム型スキル値));
                        }
                    }
                    if ( CDTXMania.stage結果.b新記録スキル.Drums || CDTXMania.stage結果.b新記録スキル.Guitar )
                    {
                        this.txNewRecord.t2D描画(CDTXMania.app.Device, this.n本体1X + 16, this.n本体1Y + 56, new Rectangle(0, 0, 111, 12));
                    }
                    if ( CDTXMania.stage結果.b新記録スキル.Bass )
                    {
                        this.txNewRecord.t2D描画(CDTXMania.app.Device, this.n本体2X + 16, this.n本体2Y + 56, new Rectangle(0, 0, 111, 12));
                    }
                    if (num >= 0)
                    {
                        if( CDTXMania.ConfigIni.bDrums有効 )
                        {
                            this.t大文字表示(x1, y1, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録.Drums.nPerfect数));
                            this.tx達成率ゲージ.t2D描画(CDTXMania.app.Device, n本体1X + 12, n本体1Y + 70, new Rectangle(0, 0, (int)dbドラムメーター, 56));
                        }
                        else
                        {
                            this.t大文字表示(x1, y1, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録.Guitar.nPerfect数));
                            this.t大文字表示(x2, y2, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録.Bass.nPerfect数));
                            this.tx達成率ゲージ.t2D描画(CDTXMania.app.Device, n本体1X + 12, n本体1Y + 70, new Rectangle(0, 0, (int)dbギターメーター, 56));
                            this.tx達成率ゲージ.t2D描画(CDTXMania.app.Device, n本体2X + 12, n本体2Y + 70, new Rectangle(0, 0, (int)dbベースメーター, 56));
                        }

                    }
                    if (num >= 100)
                    {
                        if( CDTXMania.ConfigIni.bDrums有効 )
                        {
                            this.t大文字表示(x1, y1 + 0x19, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録.Drums.nGreat数));
                        }
                        else
                        {
                            this.t大文字表示(x1, y1 + 0x19, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録.Guitar.nGreat数));
                            this.t大文字表示(x2, y2 + 0x19, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録.Bass.nGreat数));
                        }
                    }
                    if (num >= 200)
                    {
                        if (CDTXMania.ConfigIni.bDrums有効)
                        {
                            this.t大文字表示(x1, y1 + 50, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録.Drums.nGood数));
                        }
                        else
                        {
                            this.t大文字表示(x1, y1 + 50, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録.Guitar.nGood数));
                            this.t大文字表示(x2, y2 + 50, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録.Bass.nGood数));
                        }
                    }
                    if (num >= 300)
                    {
                        if( CDTXMania.ConfigIni.bDrums有効 )
                        {
                            this.t大文字表示(x1, y1 + 0x4b, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録.Drums.nPoor数));
                        }
                        else
                        {
                            this.t大文字表示(x1, y1 + 0x4b, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録.Guitar.nPoor数));
                            this.t大文字表示(x2, y2 + 0x4b, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録.Bass.nPoor数));
                        }
                    }
                    if (num >= 400)
                    {
                        if( CDTXMania.ConfigIni.bDrums有効 )
                        {
                            this.t大文字表示(x1, y1 + 100, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録.Drums.nMiss数));
                        }
                        else
                        {
                            this.t大文字表示(x1, y1 + 100, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録.Guitar.nMiss数));
                            this.t大文字表示(x2, y2 + 100, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録.Bass.nMiss数));
                        }
                    }
                    if (num >= 0)
                    {
                        if( CDTXMania.ConfigIni.bDrums有効 )
                        {
                            this.t小文字表示(x1 + 0x40, y1, string.Format("{0,3:##0}%", CDTXMania.stage結果.fPerfect率.Drums));
                        }
                        else
                        {
                            this.t小文字表示(x1 + 0x40, y1, string.Format("{0,3:##0}%", CDTXMania.stage結果.fPerfect率.Guitar));
                            this.t小文字表示(x2 + 0x40, y2, string.Format("{0,3:##0}%", CDTXMania.stage結果.fPerfect率.Bass));
                        }
                    }
                    if (num >= 100)
                    {
                        if (CDTXMania.ConfigIni.bDrums有効)
                        {
                            this.t小文字表示(x1 + 0x40, y1 + 0x19, string.Format("{0,3:##0}%", CDTXMania.stage結果.fGreat率.Drums));
                        }
                        else
                        {
                            this.t小文字表示(x1 + 0x40, y1 + 0x19, string.Format("{0,3:##0}%", CDTXMania.stage結果.fGreat率.Guitar));
                            this.t小文字表示(x2 + 0x40, y2 + 0x19, string.Format("{0,3:##0}%", CDTXMania.stage結果.fGreat率.Bass));
                        }
                    }
                    if (num >= 200)
                    {
                        if (CDTXMania.ConfigIni.bDrums有効)
                        {
                            this.t小文字表示(x1 + 0x40, y1 + 50, string.Format("{0,3:##0}%", CDTXMania.stage結果.fGood率.Drums));
                        }
                        else
                        {
                            this.t小文字表示(x1 + 0x40, y1 + 50, string.Format("{0,3:##0}%", CDTXMania.stage結果.fGood率.Guitar));
                            this.t小文字表示(x2 + 0x40, y2 + 50, string.Format("{0,3:##0}%", CDTXMania.stage結果.fGood率.Bass));
                        }
                    }
                    if (num >= 300)
                    {
                        if (CDTXMania.ConfigIni.bDrums有効)
                        {
                            this.t小文字表示(x1 + 0x40, y1 + 0x4b, string.Format("{0,3:##0}%", CDTXMania.stage結果.fPoor率.Drums));
                        }
                        else
                        {
                            this.t小文字表示(x1 + 0x40, y1 + 0x4b, string.Format("{0,3:##0}%", CDTXMania.stage結果.fPoor率.Guitar));
                            this.t小文字表示(x2 + 0x40, y2 + 0x4b, string.Format("{0,3:##0}%", CDTXMania.stage結果.fPoor率.Bass));
                        }
                    }
                    if (num >= 400)
                    {
                        if (CDTXMania.ConfigIni.bDrums有効)
                        {
                            this.t小文字表示(x1 + 0x40, y1 + 100, string.Format("{0,3:##0}%", CDTXMania.stage結果.fMiss率.Drums));
                        }
                        else
                        {
                            this.t小文字表示(x1 + 0x40, y1 + 100, string.Format("{0,3:##0}%", CDTXMania.stage結果.fMiss率.Guitar));
                            this.t小文字表示(x2 + 0x40, y2 + 100, string.Format("{0,3:##0}%", CDTXMania.stage結果.fMiss率.Bass));
                        }
                    }
                    if (num >= 500)
                    {
                        if (CDTXMania.ConfigIni.bDrums有効)
                        {
                            this.t大文字表示(x1 - 0x2c, y1 + 0x7d, string.Format("{0,9:########0}", CDTXMania.stage結果.st演奏記録.Drums.n最大コンボ数));
                            this.t小文字表示(x1 + 0x40, y1 + 0x7d, string.Format("{0,3:##0}%", (((float)CDTXMania.stage結果.st演奏記録.Drums.n最大コンボ数) / ((float)CDTXMania.stage結果.st演奏記録.Drums.n全チップ数)) * 100f));
                        }
                        else
                        {
                            this.t大文字表示(x1 - 0x2c, y1 + 0x7d, string.Format("{0,9:########0}", CDTXMania.stage結果.st演奏記録.Guitar.n最大コンボ数));
                            this.t小文字表示(x1 + 0x40, y1 + 0x7d, string.Format("{0,3:##0}%", (((float)CDTXMania.stage結果.st演奏記録.Guitar.n最大コンボ数) / ((float)CDTXMania.stage結果.st演奏記録.Guitar.n全チップ数)) * 100f));

                            this.t大文字表示(x2 - 0x2c, y2 + 0x7d, string.Format("{0,9:########0}", CDTXMania.stage結果.st演奏記録.Bass.n最大コンボ数));
                            this.t小文字表示(x2 + 0x40, y2 + 0x7d, string.Format("{0,3:##0}%", (((float)CDTXMania.stage結果.st演奏記録.Bass.n最大コンボ数) / ((float)CDTXMania.stage結果.st演奏記録.Bass.n全チップ数)) * 100f));
                        }
                    }
                    if (num >= 600)
                    {
                        if (CDTXMania.ConfigIni.bDrums有効)
                        {
                            if (CDTXMania.ConfigIni.nSkillMode == 0)
                                this.t特大文字表示(x1 - 126, y1 + 173, string.Format("{0,10:#########0}", CDTXMania.stage結果.st演奏記録.Drums.nスコア), true);
                            else
                                this.t特大文字表示(x1 - 58, y1 + 173, string.Format("{0,7:######0}", CDTXMania.stage結果.st演奏記録.Drums.nスコア), true);
                        }
                        else
                        {
                            if (CDTXMania.ConfigIni.nSkillMode == 0)
                                this.t特大文字表示(x1 - 126, y1 + 173, string.Format("{0,10:#########0}", CDTXMania.stage結果.st演奏記録.Guitar.nスコア), true);
                            else
                                this.t特大文字表示(x1 - 58, y1 + 173, string.Format("{0,7:######0}", CDTXMania.stage結果.st演奏記録.Guitar.nスコア), true);

                            if (CDTXMania.ConfigIni.nSkillMode == 0)
                                this.t特大文字表示(x2 - 126, y2 + 173, string.Format("{0,10:#########0}", CDTXMania.stage結果.st演奏記録.Bass.nスコア), true);
                            else
                                this.t特大文字表示(x2 - 58, y2 + 173, string.Format("{0,7:######0}", CDTXMania.stage結果.st演奏記録.Bass.nスコア), true);

                        }
                    }
                    if (num >= 700)
                    {
                    }
                    if (num >= 800)
                    {
                    }
                }
                if( this.ct表示用.n現在の値 < 700 )
                {
                    int num5 = this.ct表示用.n現在の値 / 100;
                    double num6 = 1.0 - (((double)(this.ct表示用.n現在の値 % 100)) / 100.0);
                    int n白1X = this.n本体1X + 393;
                    int n白1Y = this.n本体1Y + 35 + (num5 * 24);
                    int n白2X = this.n本体2X + 393;
                    int n白2Y = this.n本体2Y + 35 + (num5 * 24);
                    int height = 20;
                    if (this.txWhite != null)
                    {
                        this.txWhite.n透明度 = (int)(255.0 * num6);
                    }
                    Rectangle rectangle = new Rectangle(0, 0, 222, height);
                    if (num5 >= 2)
                    {
                        if (num5 < 3)
                        {
                            n白1Y++;
                            n白2Y++;
                        }
                        else if (num5 < 4)
                        {
                            n白1Y += 2;
                            n白2Y += 2;
                        }
                        else if (num5 < 5)
                        {
                            n白1Y += 3;
                            n白2Y += 3;
                        }
                        else if (num5 < 6)
                        {
                            n白1Y += 4;
                            n白2Y += 4;
                        }
                        else if (num5 < 7)
                        {
                            n白1Y += 5;
                            n白2Y += 5;
                            rectangle.Height = 56;
                        }
                    }
                    this.txWhite.t2D描画(CDTXMania.app.Device, n白1X, n白1Y, rectangle);

                    if ( CDTXMania.ConfigIni.bGuitar有効 )
                        this.txWhite.t2D描画(CDTXMania.app.Device, n白2X, n白2Y, rectangle);
                }

                if( this.ct表示用.n現在の値 >= 700 )
                {
                    if( CDTXMania.stage結果.b新記録スキル.Drums == true && !this.b新記録音再生済み )
                    {
                        CDTXMania.Skin.sound新記録音.t再生する();
                        this.b新記録音再生済み = true;
                    }
                }


            if (!this.ct表示用.b終了値に達した)
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

        private bool b新記録音再生済み;
        private CCounter ct表示用;
        private int n本体1X;
        private int n本体1Y;
        private int n本体2X;
        private int n本体2Y;
        private readonly ST文字位置[] st小文字位置;
        private readonly ST文字位置[] st大文字位置;
        private readonly ST文字位置[] st特大文字位置;
        private CTexture txNewRecord;
        private CTexture txWhite;
        private CTexture txパネル本体;
        private CTexture tx達成率ゲージ;
        private CTexture txDifficulty;
        private CTexture txPart;
        private CTexture txリザルトパネル;
        private CTexture[] tx文字;


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
        private void t特大文字表示(int x, int y, string str)
        {
            this.t特大文字表示(x, y, str, false);
        }
        private void t特大文字表示(int x, int y, string str, bool bExtraLarge)
        {
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                for (int j = 0; j < this.st特大文字位置.Length; j++)
                {
                    if (this.st特大文字位置[j].ch == c)
                    {
                        int num;
                        int num2;
                        if (bExtraLarge)
                        {
                            if (j < 5)
                            {
                                num = 6 * j;
                            }
                            else
                            {
                                if (j < 11)
                                {
                                    num = 6 * (j - 5);
                                }
                                else
                                {
                                    num = 24;
                                }
                            }
                            if (j < 5)
                            {
                                num2 = 48;
                            }
                            else
                            {
                                if (j < 11)
                                {
                                    num2 = 56;
                                }
                                else
                                {
                                    num2 = 48;
                                }
                            }
                        }
                        else
                        {
                            num = 0;
                            num2 = 0;
                        }
                        Rectangle rc画像内の描画領域 = new Rectangle(this.st特大文字位置[j].pt.X + num, this.st特大文字位置[j].pt.Y + num2, bExtraLarge ? 24 : 18, bExtraLarge ? 32 : 24);
                        if (c == '.')
                        {
                            rc画像内の描画領域.Width -= 2;
                            rc画像内の描画領域.Height -= 2;
                        }
                        if (this.tx文字[2] != null)
                        {
                            this.tx文字[2].t2D描画(CDTXMania.app.Device, x, y, rc画像内の描画領域);
                        }
                        break;
                    }
                }
                if (bExtraLarge)
                {
                    if (c == '.')
                    {
                        x += 20;
                    }
                    else
                    {
                        x += 23;
                    }
                }
                else
                {
                    if (c == '.')
                    {
                        x += 14;
                    }
                    else
                    {
                        x += 17;
                    }
                }
            }
        }

		//-----------------
		#endregion
	}
}
