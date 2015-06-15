using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using FDK;
using DirectShowLib;

namespace DTXMania
{
    internal class CAct演奏ステージ失敗 : CActivity
    {
        // コンストラクタ

        public CAct演奏ステージ失敗()
        {
            base.b活性化してない = true;
        }


        // メソッド

        public void Start()
        {
            this.ct進行 = new CCounter(0, 0x3e8, 2, CDTXMania.Timer);
        }


        // CActivity 実装

        public override void On活性化()
        {
            this.sd効果音 = null;
            this.b効果音再生済み = false;
            this.ct進行 = new CCounter();
            base.On活性化();
        }
        public override void On非活性化()
        {
            this.ct進行 = null;
            if (this.sd効果音 != null)
            {
                CDTXMania.Sound管理.tサウンドを破棄する(this.sd効果音);
                this.sd効果音 = null;
            }
            base.On非活性化();
        }
        public override void OnManagedリソースの作成()
        {
            if (!base.b活性化してない)
            {
                this.txStageFailed = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_stage_failed.jpg"));
                this.tx動画用背景 = new CTexture( CDTXMania.app.Device, 1280, 720, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, SlimDX.Direct3D9.Pool.Managed );
                this.ds背景動画 = CDTXMania.t失敗してもスキップ可能なDirectShowを生成する( CSkin.Path(@"Graphics\7_StageFailed.mp4"), CDTXMania.app.WindowHandle, true );
                base.OnManagedリソースの作成();
            }
        }
        public override void OnManagedリソースの解放()
        {
            if (!base.b活性化してない)
            {
                CDTXMania.tテクスチャの解放( ref this.txStageFailed );
                CDTXMania.tテクスチャの解放( ref this.tx動画用背景 );
                CDTXMania.t安全にDisposeする( ref this.ds背景動画 );
                base.OnManagedリソースの解放();
            }
        }
        public override int On進行描画()
        {
            if (base.b活性化してない)
            {
                return 0;
            }
            if ((this.ct進行 == null) || this.ct進行.b停止中)
            {
                return 0;
            }
            this.ct進行.t進行();
            if( this.ds背景動画 != null )
                this.ds背景動画.t再生開始();
            if (this.ct進行.n現在の値 < 100)
            {
                int x = (int)(640.0 * Math.Cos((Math.PI / 2 * this.ct進行.n現在の値) / 100.0));
                if ((x != 1280) && (this.txStageFailed != null))
                {
                    this.txStageFailed.t2D描画(CDTXMania.app.Device, 0, 0, new Rectangle(x, 0, 640 - x, 720));
                    this.txStageFailed.t2D描画(CDTXMania.app.Device, 640 + x, 0, new Rectangle(640, 0, 640 - x, 720));
                }
            }
            else
            {
                if (this.txStageFailed != null)
                {
                    this.txStageFailed.t2D描画(CDTXMania.app.Device, 0, 0);
                }
                if (this.ct進行.n現在の値 <= 250)
                {
                    int num2 = CDTXMania.Random.Next(5) - 2;
                    int y = CDTXMania.Random.Next(5) - 2;
                    if (this.txStageFailed != null)
                    {
                        this.txStageFailed.t2D描画(CDTXMania.app.Device, num2, y);
                    }
                }
                if (!this.b効果音再生済み)
                {
                    if (((CDTXMania.DTX.SOUND_STAGEFAILED != null) && (CDTXMania.DTX.SOUND_STAGEFAILED.Length > 0)) && File.Exists(CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.SOUND_STAGEFAILED))
                    {
                        try
                        {
                            if (this.sd効果音 != null)
                            {
                                CDTXMania.Sound管理.tサウンドを破棄する(this.sd効果音);
                                this.sd効果音 = null;
                            }
                            this.sd効果音 = CDTXMania.Sound管理.tサウンドを生成する(CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.SOUND_STAGEFAILED);
                            this.sd効果音.t再生を開始する();
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        CDTXMania.Skin.soundSTAGEFAILED音.t再生する();
                    }
                    this.b効果音再生済み = true;
                }

            }
            if( this.ds背景動画 != null && this.tx動画用背景 != null )
            {
                this.ds背景動画.t現時点における最新のスナップイメージをTextureに転写する( this.tx動画用背景 );
                if( this.ds背景動画.b上下反転 )
                {
                    this.tx動画用背景.t2D上下反転描画( CDTXMania.app.Device, 0, 0 );
                }
                else
                {
                    this.tx動画用背景.t2D描画( CDTXMania.app.Device, 0, 0 );
                }
            }

            if (!this.ct進行.b終了値に達した)
            {
                return 0;
            }
            CDTXMania.DTX.t全チップの再生停止();
            return 1;
        }


        // その他

        #region [ private ]
        //-----------------
        private bool b効果音再生済み;
        private CCounter ct進行;
        private CSound sd効果音;
        private CTexture txStageFailed;
        private CTexture tx動画用背景;
        private CDirectShow ds背景動画;
        //-----------------
        #endregion
    }
}
