using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Guitarステータスパネル : CAct演奏ステータスパネル共通
	{
        public override void On活性化()
        {

            #region [ 本体位置 ]

            {
                this.n本体1X = 402;
                this.n本体2X = 758;

                this.n本体Y = 185;

                this.nグラフNX = 313;
            }

            if (CDTXMania.ConfigIni.bGraph有効)
            {
                if (!CDTXMania.DTX.bチップがある.Bass)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体2X = this.n本体2X - this.nグラフNX;
                    }
                    else
                    {
                        this.n本体1X = this.n本体1X + this.nグラフNX;
                    }
                }
                else if (!CDTXMania.DTX.bチップがある.Guitar)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体1X = this.n本体1X + this.nグラフNX;
                    }
                    else
                    {
                        this.n本体2X = this.n本体2X - this.nグラフNX;
                    }
                }
                else if (!CDTXMania.ConfigIni.bギターが全部オートプレイである && CDTXMania.ConfigIni.bベースが全部オートプレイである)
                {
                    this.n本体1X = this.n本体1X + this.nグラフNX;
                    this.n本体2X = 0;
                }
                else if (CDTXMania.ConfigIni.bギターが全部オートプレイである && !CDTXMania.ConfigIni.bベースが全部オートプレイである)
                {
                    this.n本体2X = this.n本体2X - this.nグラフNX;
                    this.n本体1X = 0;
                }

            }

            #endregion

            base.On活性化();
        }
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.txジャケットパネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_JacketPanel.png"));
                string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PREIMAGE;
                if (!File.Exists(path))
                {
                    this.txジャケット画像 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\5_preimage default.png"));
                }
                else
                {
                    this.txジャケット画像 = CDTXMania.tテクスチャの生成(path);
                }

                this.txScore = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Score numbers_Guitar.png"));

				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                CDTXMania.tテクスチャの解放( ref this.txジャケットパネル );
                CDTXMania.tテクスチャの解放( ref this.txジャケット画像 );
                CDTXMania.tテクスチャの解放( ref this.txScore );
                base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
                if ( this.txジャケットパネル != null )
                    this.txジャケットパネル.t2D描画(CDTXMania.app.Device, 467, 287);

                if (this.txジャケット画像 != null)
                {
                    SlimDX.Matrix mat = SlimDX.Matrix.Identity;
                    mat *= SlimDX.Matrix.Scaling(245.0f / this.txジャケット画像.sz画像サイズ.Width, 245.0f / this.txジャケット画像.sz画像サイズ.Height, 1f);
                    mat *= SlimDX.Matrix.Translation(-28f, -94.5f, 0f);
                    mat *= SlimDX.Matrix.RotationZ(0.3f);

                    this.txジャケット画像.t3D描画(CDTXMania.app.Device, mat);
                }

                //CDTXMania.act文字コンソール.tPrint(0, 100, C文字コンソール.Eフォント種別.白, string.Format("{0:####0}", CDTXMania.stage演奏ギター画面.bブーストボーナス ? 1 : 0));
                if ( CDTXMania.DTX.bチップがある.Guitar && this.n本体1X != 0)
				{
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
                                rectangle = new Rectangle(0, 0, 20, 26);
                            }
                            else
                            {
                                int num3 = int.Parse(str.Substring(i, 1));
                                if (num3 < 5)
                                {
                                    rectangle = new Rectangle((num3 * 20), 0, 20, 26);
                                }
                                else
                                {
                                    rectangle = new Rectangle((num3 * 20), 0, 20, 26);
                                }
                            }
                            if (this.txScore != null)
                            {
                                SlimDX.Matrix matScoreXG = SlimDX.Matrix.Identity;
                                matScoreXG *= SlimDX.Matrix.Translation(((-640 + 8 + this.n本体1X) / 0.7f) + (i * 20), 360 - (12 + this.n本体Y), 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(0.7f, 1f, 1f);
                                this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);

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
                                rectangle = new Rectangle(0, 0, 20, 26);
                            }
                            else
                            {
                                int num3 = int.Parse(str.Substring(i, 1));
                                if (num3 < 5)
                                {
                                    rectangle = new Rectangle((num3 * 20), 0, 20, 26);
                                }
                                else
                                {
                                    rectangle = new Rectangle((num3 * 20), 0, 20, 26);
                                }
                            }
                            if ( this.txScore != null )
                            {
                                this.txScore.t2D描画(CDTXMania.app.Device, this.n本体1X + (i * 20), this.n本体Y, rectangle);
                            }
                        }
                    }
                    #endregion
				}

                if ( CDTXMania.DTX.bチップがある.Bass && this.n本体2X != 0 )
                {
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
                                rectangle = new Rectangle(0, 0, 20, 26);
                            }
                            else
                            {
                                int num3 = int.Parse(str.Substring(i, 1));
                                if (num3 < 5)
                                {
                                    rectangle = new Rectangle((num3 * 20), 0, 20, 26);
                                }
                                else
                                {
                                    rectangle = new Rectangle((num3 * 20), 0, 20, 26);
                                }
                            }
                            if (this.txScore != null)
                            {
                                SlimDX.Matrix matScoreXG = SlimDX.Matrix.Identity;
                                matScoreXG *= SlimDX.Matrix.Translation(((-640 + 8 + this.n本体2X) / 0.7f) + (i * 20), 360 - (12 + this.n本体Y), 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(0.7f, 1f, 1f);
                                this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
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
                                rectangle = new Rectangle(0, 0, 20, 26);
                            }
                            else
                            {
                                int num3 = int.Parse(str.Substring(i, 1));
                                if (num3 < 5)
                                {
                                    rectangle = new Rectangle((num3 * 20), 0, 20, 26);
                                }
                                else
                                {
                                    rectangle = new Rectangle((num3 * 20), 0, 20, 26);
                                }
                            }
                            if (this.txScore != null)
                            {
                                this.txScore.t2D描画(CDTXMania.app.Device, this.n本体2X + (i * 20), this.n本体Y, rectangle);
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

		//private STATUSPANEL[] stパネルマップ;
        private int n本体1X;
        private int n本体2X;
        private int n本体Y;
        private int nグラフNX;
        private CTexture txジャケットパネル;
        private CTexture txジャケット画像;
        private CTexture txScore;
        //-----------------
		#endregion
	}
}
