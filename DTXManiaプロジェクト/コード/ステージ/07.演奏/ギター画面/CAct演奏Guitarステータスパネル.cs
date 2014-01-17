using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Guitarステータスパネル : CAct演奏ステータスパネル共通
	{
        public override void On活性化()
        {

            #region [ 本体位置 ]

            int nグラフX = 313;
            int nグラフ0X = 135;

            this.n本体0X = 515;
            this.n本体0Y = 521;

            this.n本体1X = 337;
            this.n本体2X = 693;

            this.n本体Y = 211;

            if (!CDTXMania.DTX.bチップがある.Guitar || !CDTXMania.DTX.bチップがある.Bass)
            {
                if (CDTXMania.ConfigIni.bGraph.Guitar)
                {
                    this.n本体1X = this.n本体1X + nグラフX;
                }

                if (CDTXMania.ConfigIni.bGraph.Bass)
                {
                    this.n本体2X = this.n本体2X - nグラフX;
                }

                if (CDTXMania.ConfigIni.bGraph.Guitar || CDTXMania.ConfigIni.bGraph.Bass)
                {
                    this.n本体0X = this.n本体0X + nグラフ0X;
                }


            }

            #endregion

            this.ftDisplayFont = new Font("ＤＦＧ平成ゴシック体W5", 18f, FontStyle.Regular, GraphicsUnit.Pixel);
            this.ftGroupFont = new Font("ＤＦＧ平成ゴシック体W5", 16f, FontStyle.Regular, GraphicsUnit.Pixel);
            this.ftNameFont = new Font("Arial", 26f, FontStyle.Bold, GraphicsUnit.Pixel);
            this.ftLevelFont = new Font("Impact", 26f, FontStyle.Regular);
            this.ftDifficultyL = new Font("Arial", 30f, FontStyle.Bold);
            this.ftDifficultyS = new Font("Arial", 20f, FontStyle.Bold);
            base.On活性化();
        }
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.txScore = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Score numbers_Guitar.png" ) );
                this.b4font = new Bitmap(1, 1);
                Graphics graphics = Graphics.FromImage(this.b4font);
                graphics.PageUnit = GraphicsUnit.Pixel;

                this.strPlayerName = string.IsNullOrEmpty(CDTXMania.ConfigIni.strCardName) ? "GUEST" : CDTXMania.ConfigIni.strCardName;
                this.strGroupName = string.IsNullOrEmpty(CDTXMania.ConfigIni.strGroupName) ? "" : CDTXMania.ConfigIni.strGroupName;

                //ギターベースの有無を判断する仕様を考えて、生成などは分けておく。

                #region[ ギターNamePlate ]
                this.iNamePlate = Image.FromFile(CSkin.Path(@"Graphics\7_nameplate_Guitar.png"));
                Graphics gNamePlate = Graphics.FromImage(this.b4font);
                this.bNamePlate = new Bitmap( 250, 266 );
                gNamePlate = Graphics.FromImage( this.bNamePlate );
                gNamePlate.Dispose();
                gNamePlate = Graphics.FromImage( this.bNamePlate );
                gNamePlate.DrawImage( this.iNamePlate, 0, 0, 250, 266 );
                gNamePlate.DrawString(this.strPlayerName, this.ftNameFont, Brushes.White, (float)48f, (float)57f);

                gNamePlate.Dispose();
                this.iNamePlate.Dispose();
                #endregion

                this.iSongPanel = Image.FromFile(CSkin.Path(@"Graphics\7_songpanel.png"));
                Graphics gSongPanel = Graphics.FromImage(this.b4font);
                this.strPanelString = string.IsNullOrEmpty(CDTXMania.DTX.TITLE) ? "No Song Name" : CDTXMania.stage選曲.r確定された曲.strタイトル;
                this.bSongPanel = new Bitmap(250, 112);

                string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PREIMAGE;
                if( !File.Exists( path ) )
                {
                    this.iAlbum = Image.FromFile( CSkin.Path( @"Graphics\5_preimage default.png" ) );
                }
                else
                {
                    this.iAlbum = Image.FromFile( path );
                }

                gSongPanel.Dispose();
                gSongPanel = Graphics.FromImage(this.bSongPanel);
                gSongPanel.DrawImage( this.iSongPanel, 0, 0, 250, 112 );
                gSongPanel.DrawImage( this.iAlbum, new Rectangle( 20, 10, 51, 51 ), new Rectangle( 0, 0, this.iAlbum.Width, this.iAlbum.Height ), GraphicsUnit.Pixel );
                gSongPanel.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, (float)16f, (float)78f);

				//this.txパネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_nameplate_Guitar.png" ) );
                this.txパネル = new CTexture( CDTXMania.app.Device, this.bNamePlate, CDTXMania.TextureFormat, false );
                //this.tx曲名パネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_songpanel.png"));
                this.tx曲名パネル = new CTexture(CDTXMania.app.Device, this.bSongPanel, CDTXMania.TextureFormat, false);

                gSongPanel.Dispose();
                this.iSongPanel.Dispose();

                this.ftDisplayFont.Dispose();
                this.ftNameFont.Dispose();

				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txパネル );
                CDTXMania.tテクスチャの解放( ref this.tx曲名パネル );
                CDTXMania.tテクスチャの解放( ref this.txScore );

                this.ftDisplayFont.Dispose();
                this.ftNameFont.Dispose();
                this.ftLevelFont.Dispose();
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{

                this.tx曲名パネル.t2D描画(CDTXMania.app.Device, this.n本体0X, this.n本体0Y);

                //CDTXMania.act文字コンソール.tPrint(0, 100, C文字コンソール.Eフォント種別.白, string.Format("{0:####0}", CDTXMania.stage演奏ギター画面.bブーストボーナス ? 1 : 0));
				if( this.txパネル != null && CDTXMania.DTX.bチップがある.Guitar )
				{
			        this.txパネル.t2D描画( CDTXMania.app.Device, this.n本体1X, this.n本体Y );
					int guitar = CDTXMania.ConfigIni.n譜面スクロール速度.Guitar;
                    #region[ スコア表示 ]
                    this.n現在のスコアGuitar = (long)CDTXMania.stage演奏ギター画面.actScore.n現在表示中のスコア.Guitar;
                    if( CDTXMania.ConfigIni.nSkillMode == 0 )
                    {
                        string str = this.n現在のスコアGuitar.ToString("0000000000");
                        for (int i = 0; i < 10; i++)
                        {
                            Rectangle rectangle;
                            char ch = str[i];
                            if (ch.Equals(' '))
                            {
                                rectangle = new Rectangle(0, 0, 24, 25);
                            }
                            else
                            {
                                int num3 = int.Parse(str.Substring(i, 1));
                                if (num3 < 5)
                                {
                                    rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                                }
                                else
                                {
                                    rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                                }
                            }
                            if (this.txScore != null)
                            {
                                this.txScore.t2D描画(CDTXMania.app.Device, 65 + this.n本体1X + (i * 25), 185 + this.n本体Y, rectangle);

                                /*
                                SlimDX.Matrix matScoreXG = SlimDX.Matrix.Identity;
                                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                                {
                                    matScoreXG *= SlimDX.Matrix.Translation(-1220 + (i * 30), 120 + CDTXMania.stage演奏ギター画面.actScore.x位置[i].Guitar, 0);
                                    matScoreXG *= SlimDX.Matrix.Scaling(0.34f, 0.62f, 1.0f);
                                    matScoreXG *= SlimDX.Matrix.RotationY(-0.60f);
                                    this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                                }
                                else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                                {
                                    matScoreXG *= SlimDX.Matrix.Translation(-1370 + (i * 30), 50 + CDTXMania.stage演奏ギター画面.actScore.x位置[i].Guitar, 0);
                                    matScoreXG *= SlimDX.Matrix.Scaling(0.3f, 0.62f, 1f);
                                    matScoreXG *= SlimDX.Matrix.RotationY(-0.8f);
                                    //matScoreXG *= SlimDX.Matrix.RotationZ(-0.01f);
                                    this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                                 }
                                 */

                            }
                        }
                    }
                    else if( CDTXMania.ConfigIni.nSkillMode == 1 )
                    {
                        string str = this.n現在のスコアGuitar.ToString("0000000");
                        for( int i = 0; i < 7; i++ )
                        {
                            Rectangle rectangle;
                            char ch = str[i];
                            if (ch.Equals(' '))
                            {
                                rectangle = new Rectangle(0, 0, 24, 25);
                            }
                            else
                            {
                                int num3 = int.Parse(str.Substring(i, 1));
                                if (num3 < 5)
                                {
                                    rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                                }
                                else
                                {
                                    rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                                }
                            }
                            if( this.txScore != null )
                            {
                                this.txScore.t2D描画(CDTXMania.app.Device, 65 + this.n本体1X + (i * 25), 185 + this.n本体Y, rectangle);
                                /*
                                SlimDX.Matrix matScoreXG = SlimDX.Matrix.Identity;
                                if( CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A )
                                {
                                    //matScoreXG *= SlimDX.Matrix.Translation(-870 + (i * 30), 114 + CDTXMania.stage演奏ドラム画面.actScore.x位置[i].Drums, 0);
                                    //matScoreXG *= SlimDX.Matrix.Scaling(0.47f, 0.65f, 1.0f);
                                    //matScoreXG *= SlimDX.Matrix.RotationY(-0.60f);
                                    this.txScore.t2D描画(CDTXMania.app.Device, 65 + this.n本体1X + (i * 25), 185 + this.n本体Y, rectangle);
                                }
                                else if( CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B )
                                {
                                    matScoreXG *= SlimDX.Matrix.Translation(-974 + (i * 30), 50 + CDTXMania.stage演奏ギター画面.actScore.x位置[i].Guitar, 0);
                                    matScoreXG *= SlimDX.Matrix.Scaling(0.42f, 0.62f, 1f);
                                    matScoreXG *= SlimDX.Matrix.RotationY(-0.8f);
                                    //matScoreXG *= SlimDX.Matrix.RotationZ(-0.01f);
                                    this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                                }
                                 */
                            }
                        }
                    }
                    #endregion
				}

                if (this.txパネル != null && CDTXMania.DTX.bチップがある.Bass)
                {
                    this.txパネル.t2D描画(CDTXMania.app.Device, this.n本体2X, this.n本体Y);
                    int bass = CDTXMania.ConfigIni.n譜面スクロール速度.Bass;
                    #region[ スコア表示 ]
                    this.n現在のスコアBass = (long)CDTXMania.stage演奏ギター画面.actScore.n現在表示中のスコア.Bass;
                    if (CDTXMania.ConfigIni.nSkillMode == 0)
                    {
                        string str = this.n現在のスコアBass.ToString("0000000000");
                        for (int i = 0; i < 10; i++)
                        {
                            Rectangle rectangle;
                            char ch = str[i];
                            if (ch.Equals(' '))
                            {
                                rectangle = new Rectangle(0, 0, 24, 25);
                            }
                            else
                            {
                                int num3 = int.Parse(str.Substring(i, 1));
                                if (num3 < 5)
                                {
                                    rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                                }
                                else
                                {
                                    rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                                }
                            }
                            if (this.txScore != null)
                            {
                                this.txScore.t2D描画(CDTXMania.app.Device, 65 + this.n本体2X + (i * 25), 185 + this.n本体Y, rectangle);

                                /*
                                SlimDX.Matrix matScoreXG = SlimDX.Matrix.Identity;
                                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                                {
                                    matScoreXG *= SlimDX.Matrix.Translation(-1220 + (i * 30), 120 + CDTXMania.stage演奏ギター画面.actScore.x位置[i].Bass, 0);
                                    matScoreXG *= SlimDX.Matrix.Scaling(0.34f, 0.62f, 1.0f);
                                    matScoreXG *= SlimDX.Matrix.RotationY(-0.60f);
                                    this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                                }
                                else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                                {
                                    matScoreXG *= SlimDX.Matrix.Translation(-1370 + (i * 30), 50 + CDTXMania.stage演奏ギター画面.actScore.x位置[i].Bass, 0);
                                    matScoreXG *= SlimDX.Matrix.Scaling(0.3f, 0.62f, 1f);
                                    matScoreXG *= SlimDX.Matrix.RotationY(-0.8f);
                                    //matScoreXG *= SlimDX.Matrix.RotationZ(-0.01f);
                                    this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                                }
                                 */
                            }
                        }
                    }
                    else if (CDTXMania.ConfigIni.nSkillMode == 1)
                    {
                        string str = this.n現在のスコアBass.ToString("0000000");
                        for (int i = 0; i < 7; i++)
                        {
                            Rectangle rectangle;
                            char ch = str[i];
                            if (ch.Equals(' '))
                            {
                                rectangle = new Rectangle(0, 0, 24, 25);
                            }
                            else
                            {
                                int num3 = int.Parse(str.Substring(i, 1));
                                if (num3 < 5)
                                {
                                    rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                                }
                                else
                                {
                                    rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                                }
                            }
                            if (this.txScore != null)
                            {
                                this.txScore.t2D描画(CDTXMania.app.Device, 65 + this.n本体2X + (i * 25), 185 + this.n本体Y, rectangle);
                                /*
                                SlimDX.Matrix matScoreXG = SlimDX.Matrix.Identity;
                                if( CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A )
                                {
                                    //matScoreXG *= SlimDX.Matrix.Translation(-870 + (i * 30), 114 + CDTXMania.stage演奏ドラム画面.actScore.x位置[i].Drums, 0);
                                    //matScoreXG *= SlimDX.Matrix.Scaling(0.47f, 0.65f, 1.0f);
                                    //matScoreXG *= SlimDX.Matrix.RotationY(-0.60f);
                                    this.txScore.t2D描画(CDTXMania.app.Device, 65 + this.n本体1X + (i * 25), 185 + this.n本体Y, rectangle);
                                }
                                else if( CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B )
                                {
                                    matScoreXG *= SlimDX.Matrix.Translation(-974 + (i * 30), 50 + CDTXMania.stage演奏ギター画面.actScore.x位置[i].Guitar, 0);
                                    matScoreXG *= SlimDX.Matrix.Scaling(0.42f, 0.62f, 1f);
                                    matScoreXG *= SlimDX.Matrix.RotationY(-0.8f);
                                    //matScoreXG *= SlimDX.Matrix.RotationZ(-0.01f);
                                    this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                                }
                                 */
                            }
                        }
                    }
                    #endregion
                }

			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		//[StructLayout( LayoutKind.Sequential )]
		//private struct STATUSPANEL
		//{
		//    public string label;
		//    public int status;
		//}

		//private int nStatus;
		//private STATUSPANEL[] stパネルマップ;
        private int n本体0X;
        private int n本体0Y;
        private int n本体1X;
        private int n本体2X;
        private int n本体Y;
		private CTexture txパネル;
        private CTexture tx曲名パネル;
        private Bitmap b4font;
        private Bitmap bNamePlate;
        private Bitmap bSongPanel;
        //private Bitmap bDifficulty;
        private Font ftDifficultyL;
        private Font ftDifficultyS;
        private Font ftDisplayFont;
        private Font ftNameFont;
        private Font ftGroupFont;
        private Font ftLevelFont;
        private string strGroupName;
        private string strPanelString;
        private string strPlayerName;
        private Image iAlbum;
        //private Image iDrumspeed;
        //private Image iRisky;
        private Image iNamePlate;
        private Image iSongPanel;
        //private Image iDifficulty;
        private CTexture txScore;
        //private CTexture txDummy;
        //private CTexture txDifficulty;
		//-----------------
		#endregion
	}
}
