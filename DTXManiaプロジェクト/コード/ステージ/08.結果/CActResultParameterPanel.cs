﻿using System;
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

            this.n本体X[0] = 0;
            this.n本体Y[0] = 0;

            this.n本体X[1] = 0;
            this.n本体Y[1] = 0;

            this.n本体X[2] = 0;
            this.n本体Y[2] = 0;

            if (CDTXMania.ConfigIni.bDrums有効)
            {
                this.n本体X[0] = n上X;
                this.n本体Y[0] = n上Y;
            }
            else if (CDTXMania.ConfigIni.bGuitar有効)
            {
                if (CDTXMania.DTX.bチップがある.Guitar)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体X[1] = n下X;
                        this.n本体Y[1] = n下Y;
                    }
                    else
                    {
                        this.n本体X[1] = n上X;
                        this.n本体Y[1] = n上Y;
                    }
                }

                if (CDTXMania.DTX.bチップがある.Bass)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体X[2] = n上X;
                        this.n本体Y[2] = n上Y;
                    }
                    else
                    {
                        this.n本体X[2] = n下X;
                        this.n本体Y[2] = n下Y;
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
                this.txHiSpeed = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_panel_icons.jpg"));
                this.txRisky = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_panel_icons2.jpg"));

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
                CDTXMania.tテクスチャの解放( ref this.txDifficulty );
                CDTXMania.tテクスチャの解放( ref this.txPart );
                CDTXMania.tテクスチャの解放( ref this.txHiSpeed );
                CDTXMania.tテクスチャの解放( ref this.txRisky );
                CDTXMania.tテクスチャの解放( ref this.txリザルトパネル );
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

            int[] z = new int[3];

            if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
            {
                z[0] = 0;
                z[1] = 2;
                z[2] = 1;
            }
            else
            {
                z[0] = 0;
                z[1] = 1;
                z[2] = 2;
            }

            double[] dbメーター = new double[3];

            this.txHiSpeed.vc拡大縮小倍率 = new SlimDX.Vector3(32.0f / 42.0f, 32.0f / 48.0f, 1.0f);
            this.txRisky.vc拡大縮小倍率 = new SlimDX.Vector3(32.0f / 42.0f, 32.0f / 48.0f, 1.0f);

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

            for (int i = 0; i < 3; i++)
            {

                if (this.n本体X[i] != 0)
                {

                    dbメーター[i] = ((3.5 * ((CDTXMania.stage結果.st演奏記録[i].b全AUTOじゃない ? CDTXMania.stage結果.st演奏記録[i].db演奏型スキル値 : 100.0))) / 500.0) * (this.ct表示用.n現在の値 > 500 ? 500 : this.ct表示用.n現在の値);

                    if (this.txリザルトパネル != null)
                        this.txリザルトパネル.t2D描画(CDTXMania.app.Device, this.n本体X[i], this.n本体Y[i]);

                    //if (this.txDifficulty != null)
                    //    this.txDifficulty.t2D描画(CDTXMania.app.Device, this.n本体X[i] + 510, this.n本体Y[i] + 8, new Rectangle(0, CDTXMania.nSongDifficulty * 20, 120, 20));
                    this.t難易度パネルを描画する( CDTXMania.stage選曲.r確定された曲.ar難易度ラベル[ CDTXMania.stage選曲.n確定された曲の難易度 ], 941, 19 );

                    if (this.txPart != null)
                        this.txPart.t2D描画(CDTXMania.app.Device, this.n本体X[i] + 388, this.n本体Y[i] + 8, new Rectangle(0, 0 + (20 * i), 120, 20));

                    if (this.txHiSpeed != null)
                        this.txHiSpeed.t2D描画(CDTXMania.app.Device, this.n本体X[i] + 631, this.n本体Y[i] + 49, new Rectangle(0, CDTXMania.ConfigIni.n譜面スクロール速度[z[i]] * 0x30, 0x2a, 0x30));

                    if (this.txRisky != null)
                        this.txRisky.t2D描画(CDTXMania.app.Device, this.n本体X[i] + 631, this.n本体Y[i] + 83, new Rectangle(0, ((CDTXMania.ConfigIni.nRisky > 10) ? 10 : CDTXMania.ConfigIni.nRisky) * 48, 42, 48));

                    if (CDTXMania.stage結果.st演奏記録[i].nPerfect数 == CDTXMania.stage結果.st演奏記録[i].n全チップ数)
                    {
                        this.txNewRecord.t2D描画(CDTXMania.app.Device, this.n本体X[i] + 79, this.n本体Y[i] + 29, new Rectangle(0, 12, 72, 26));
                        this.t特大文字表示(this.n本体X[i] + 259, this.n本体Y[i] + 32, string.Format("{0,6:##0.00}", CDTXMania.stage結果.st演奏記録[i].dbゲーム型スキル値));
                    }
                    else
                    {
                        this.t特大文字表示(this.n本体X[i] + 69, this.n本体Y[i] + 31, string.Format("{0,-6:##0.00%}", CDTXMania.stage結果.st演奏記録[i].db演奏型スキル値 / 100.0));
                        this.t特大文字表示(this.n本体X[i] + 259, this.n本体Y[i] + 32, string.Format("{0,6:##0.00}", CDTXMania.stage結果.st演奏記録[i].dbゲーム型スキル値));
                    }

                    if (CDTXMania.stage結果.b新記録スキル[i])
                    {
                        this.txNewRecord.t2D描画(CDTXMania.app.Device, this.n本体X[i] + 16, this.n本体Y[i] + 56, new Rectangle(0, 0, 111, 12));
                    }

                    if (num >= 0)
                    {
                        this.t大文字表示(this.n本体X[i] + 507, this.n本体Y[i] + 35, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録[i].nPerfect数));
                        this.tx達成率ゲージ.t2D描画(CDTXMania.app.Device, this.n本体X[i] + 12, this.n本体Y[i] + 70, new Rectangle(0, 0, (int)dbメーター[i], 56));
                        this.t小文字表示(this.n本体X[i] + 507 + 0x40, this.n本体Y[i] + 35, string.Format("{0,3:##0}%", CDTXMania.stage結果.fPerfect率[i]));
                    }
                    if (num >= 100)
                    {
                        this.t大文字表示(this.n本体X[i] + 507, this.n本体Y[i] + 35 + 25, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録[i].nGreat数));
                        this.t小文字表示(this.n本体X[i] + 507 + 0x40, this.n本体Y[i] + 35 + 0x19, string.Format("{0,3:##0}%", CDTXMania.stage結果.fGreat率[i]));
                    }
                    if (num >= 200)
                    {
                        this.t大文字表示(this.n本体X[i] + 507, this.n本体Y[i] + 35 + 50, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録[i].nGood数));
                        this.t小文字表示(this.n本体X[i] + 507 + 0x40, this.n本体Y[i] + 35 + 50, string.Format("{0,3:##0}%", CDTXMania.stage結果.fGood率[i]));
                    }
                    if (num >= 300)
                    {
                        this.t大文字表示(this.n本体X[i] + 507, this.n本体Y[i] + 35 + 75, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録[i].nPoor数));
                        this.t小文字表示(this.n本体X[i] + 507 + 0x40, this.n本体Y[i] + 35 + 0x4b, string.Format("{0,3:##0}%", CDTXMania.stage結果.fPoor率[i]));
                    }
                    if (num >= 400)
                    {
                        this.t大文字表示(this.n本体X[i] + 507, this.n本体Y[i] + 35 + 100, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録[i].nMiss数));
                        this.t小文字表示(this.n本体X[i] + 507 + 0x40, this.n本体Y[i] + 35 + 100, string.Format("{0,3:##0}%", CDTXMania.stage結果.fMiss率[i]));
                    }
                    if (num >= 500)
                    {
                        this.t大文字表示(this.n本体X[i] + 507 - 0x2c, this.n本体Y[i] + 35 + 0x7d, string.Format("{0,9:########0}", CDTXMania.stage結果.st演奏記録[i].n最大コンボ数));
                        this.t小文字表示(this.n本体X[i] + 507 + 0x40, this.n本体Y[i] + 35 + 0x7d, string.Format("{0,3:##0}%", (((float)CDTXMania.stage結果.st演奏記録[i].n最大コンボ数) / ((float)CDTXMania.stage結果.st演奏記録[i].n全チップ数)) * 100f));
                    }
                    if (num >= 600)
                    {
                        if (CDTXMania.ConfigIni.nSkillMode == 0)
                            this.t特大文字表示(this.n本体X[i] + 507 - 126, this.n本体Y[i] + 35 + 173, string.Format("{0,10:#########0}", CDTXMania.stage結果.st演奏記録[i].nスコア), true);
                        else
                            this.t特大文字表示(this.n本体X[i] + 507 - 58, this.n本体Y[i] + 35 + 173, string.Format("{0,7:######0}", CDTXMania.stage結果.st演奏記録[i].nスコア), true);
                    }
                    if (this.ct表示用.n現在の値 >= 700)
                    {
                        if (CDTXMania.stage結果.b新記録スキル[i] == true && !this.b新記録音再生済み)
                        {
                            CDTXMania.Skin.sound新記録音.t再生する();
                            this.b新記録音再生済み = true;
                        }
                    }
                }
            }
            
                int num5 = this.ct表示用.n現在の値 / 100;
                double num6 = 1.0 - (((double)(this.ct表示用.n現在の値 % 100)) / 100.0);
                int height = 20;

            for (int i = 0; i < 3; i++)
            {
                    this.n白X[i] = this.n本体X[i] + 393;
                    this.n白Y[i] = this.n本体Y[i] + 35 + (num5 * 24);

                if (this.n本体X[i] != 0)
                {
                    if (this.ct表示用.n現在の値 < 700)
                    {
                        if (this.txWhite != null)
                        {
                            this.txWhite.n透明度 = (int)(255.0 * num6);
                        }
                        Rectangle rectangle = new Rectangle(0, 0, 222, height);
                        if (num5 >= 2)
                        {
                            if (num5 < 3)
                            {
                                this.n白Y[i]++;
                            }
                            else if (num5 < 4)
                            {
                                this.n白Y[i] += 2;
                            }
                            else if (num5 < 5)
                            {
                                this.n白Y[i] += 3;
                            }
                            else if (num5 < 6)
                            {
                                this.n白Y[i] += 4;
                            }
                            else if (num5 < 7)
                            {
                                this.n白Y[i] += 5;
                                rectangle.Height = 56;
                            }
                        }
                        this.txWhite.t2D描画(CDTXMania.app.Device, this.n白X[i], this.n白Y[i], rectangle);
                    }
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
        private STDGBVALUE<int> n本体X;
        private STDGBVALUE<int> n本体Y;
        private STDGBVALUE<int> n白X;
        private STDGBVALUE<int> n白Y;
        private readonly ST文字位置[] st小文字位置;
        private readonly ST文字位置[] st大文字位置;
        private readonly ST文字位置[] st特大文字位置;
        private CTexture txNewRecord;
        private CTexture txWhite;
        private CTexture txパネル本体;
        private CTexture tx達成率ゲージ;
        private CTexture txDifficulty;
        private CTexture txHiSpeed;
        private CTexture txPart;
        private CTexture txRisky;
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
        private void t難易度パネルを描画する( string strラベル名, int nX, int nY )
        {
            string strRawScriptFile;

            Rectangle rect = new Rectangle( 0, 0, 120, 20 );

            //ファイルの存在チェック
            if( File.Exists( CSkin.Path( @"Script\difficult.dtxs" ) ) )
            {
                //スクリプトを開く
                StreamReader reader = new StreamReader( CSkin.Path( @"Script\difficult.dtxs" ), Encoding.GetEncoding( "Shift_JIS" ) );
                strRawScriptFile = reader.ReadToEnd();

                strRawScriptFile = strRawScriptFile.Replace( Environment.NewLine, "\n" );
                string[] delimiter = { "\n" };
                string[] strSingleLine = strRawScriptFile.Split( delimiter, StringSplitOptions.RemoveEmptyEntries );

                for( int i = 0; i < strSingleLine.Length; i++ )
                {
                    if( strSingleLine[ i ].StartsWith( "//" ) )
                        continue; //コメント行の場合は無視

                    //まずSplit
                    string[] arScriptLine = strSingleLine[ i ].Split( ',' );

                    if( ( arScriptLine.Length >= 4 && arScriptLine.Length <= 5 ) == false )
                        continue; //引数が4つか5つじゃなければ無視。

                    if( arScriptLine[ 0 ] != "8" )
                        continue; //使用するシーンが違うなら無視。

                    if( arScriptLine.Length == 4 )
                    {
                        if( String.Compare( arScriptLine[ 1 ], strラベル名, true ) != 0 )
                            continue; //ラベル名が違うなら無視。大文字小文字区別しない
                    }
                    else if( arScriptLine.Length == 5 )
                    {
                        if( arScriptLine[ 4 ] == "1" )
                        {
                            if( arScriptLine[ 1 ] != strラベル名 )
                                continue; //ラベル名が違うなら無視。
                        }
                        else
                        {
                            if( String.Compare( arScriptLine[ 1 ], strラベル名, true ) != 0 )
                                continue; //ラベル名が違うなら無視。大文字小文字区別しない
                        }
                    }
                    rect.X = Convert.ToInt32( arScriptLine[ 2 ] );
                    rect.Y = Convert.ToInt32( arScriptLine[ 3 ] );

                    break;
                }
            }

            if( this.txDifficulty != null )
                this.txDifficulty.t2D描画( CDTXMania.app.Device, nX, nY, rect );
        }
		//-----------------
		#endregion
	}
}
