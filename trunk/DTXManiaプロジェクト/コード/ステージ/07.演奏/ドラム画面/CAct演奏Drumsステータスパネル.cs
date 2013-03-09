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
    internal class CAct演奏Drumsステータスパネル : CAct演奏ステータスパネル共通
    {
        public override void On活性化()
        {

            this.ftDisplayFont = new Font("ＤＦＧ平成ゴシック体W5", 22f, FontStyle.Regular, GraphicsUnit.Pixel);
            this.ftGroupFont = new Font("ＤＦＧ平成ゴシック体W5", 16f, FontStyle.Regular, GraphicsUnit.Pixel);
            this.ftNameFont = new Font("Arial", 26f, FontStyle.Bold, GraphicsUnit.Pixel);
            this.ftLevelFont = new Font("Impact", 26f, FontStyle.Regular);
            this.ftDifficultyL = new Font("Arial", 30f, FontStyle.Bold);
            this.ftDifficultyS = new Font("Arial", 20f, FontStyle.Bold);
            if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
            {
                if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && (CDTXMania.DTX.bチップがある.LeftCymbal == false) && (CDTXMania.DTX.bチップがある.LP == false) && (CDTXMania.DTX.bチップがある.LBD == false) && (CDTXMania.DTX.bチップがある.FT == false) && (CDTXMania.DTX.bチップがある.Ride == false))
                {
                    this.ftDifficultyL = new Font("Arial", 30f, FontStyle.Bold);
                    this.ftDifficultyS = new Font("Arial", 20f, FontStyle.Bold);
                }
                else
                {
                    this.ftDifficultyL = new Font("Arial", 48f, FontStyle.Bold);
                    this.ftDifficultyS = new Font("Arial", 20f, FontStyle.Bold);
                }
            }
            CDTXMania.nSongDifficulty = this.nStatus;
            CDTXMania.strSongDifficulyName = this.stパネルマップ[this.nStatus].label;
            base.On活性化();
        }
        public override void OnManagedリソースの作成()
        {
            if (!base.b活性化してない)
            {
                this.txScore = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_score numbers.png"));
                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                {
                    this.txDifficulty = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_Difficulty_XG.png"));
                }
                else
                {
                    this.txDifficulty = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_Difficulty.png"));
                }
                this.iDifficulty = Image.FromFile(CSkin.Path(@"Graphics\7_Dummy.png"));
                #region[ ネームプレート本体 ]
                this.iNamePlate = Image.FromFile(CSkin.Path(@"Graphics\7_Dummy.png"));
                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A || CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.C)
                {
                    this.iNamePlate = Image.FromFile(CSkin.Path(@"Graphics\\7_nameplate.png"));
                    if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && (CDTXMania.DTX.bチップがある.LeftCymbal == false) && (CDTXMania.DTX.bチップがある.LP == false) && (CDTXMania.DTX.bチップがある.LBD == false) && (CDTXMania.DTX.bチップがある.FT == false) && (CDTXMania.DTX.bチップがある.Ride == false))
                    {
                        if (this.iNamePlate != null)
                        this.iNamePlate = Image.FromFile(CSkin.Path(@"Graphics\\7_nameplate_cls.png"));
                    }
                }
                else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                {
                    this.iNamePlate = Image.FromFile(CSkin.Path(@"Graphics\\7_nameplate_XG.png"));
                    if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && (CDTXMania.DTX.bチップがある.LeftCymbal == false) && (CDTXMania.DTX.bチップがある.LP == false) && (CDTXMania.DTX.bチップがある.LBD == false) && (CDTXMania.DTX.bチップがある.FT == false) && (CDTXMania.DTX.bチップがある.Ride == false))
                    {
                        this.iNamePlate = Image.FromFile(CSkin.Path(@"Graphics\\7_nameplate_XG_cls.png"));
                    }
                }
                #endregion
                this.iDrumspeed = Image.FromFile(CSkin.Path(@"Graphics\\7_panel_icons.jpg"));
                this.iRisky = Image.FromFile(CSkin.Path(@"Graphics\\7_panel_icons2.jpg"));
                this.iScore = Image.FromFile(CSkin.Path(@"Graphics\\ScreenPlay score numbers.png"));
                this.b4font = new Bitmap(1, 1);
                Graphics graphics = Graphics.FromImage(this.b4font);
                graphics.PageUnit = GraphicsUnit.Pixel;

                Graphics graphics2 = Graphics.FromImage(this.b4font);
                graphics2.PageUnit = GraphicsUnit.Pixel;
                this.strPanelString = string.IsNullOrEmpty(CDTXMania.DTX.TITLE) ? "No Song Name" : CDTXMania.stage選曲.r確定された曲.strタイトル;
                this.strPlayerName = string.IsNullOrEmpty(CDTXMania.ConfigIni.strCardName) ? "GUEST" : CDTXMania.ConfigIni.strCardName;
                this.strGroupName = string.IsNullOrEmpty(CDTXMania.ConfigIni.strGroupName) ? "" : CDTXMania.ConfigIni.strGroupName;
                this.nStrlengthbydot = (int)graphics.MeasureString(this.strPanelString, this.ftDisplayFont).Width;
                graphics.Dispose();
                this.bNamePlate = new Bitmap(0x170, 0x103);
                this.bDifficulty = new Bitmap(0x170, 0x103);
                graphics = Graphics.FromImage(this.bNamePlate);
                graphics2 = Graphics.FromImage(this.bDifficulty);
                graphics.DrawImage(this.iNamePlate, 0, 0, 0x170, 0x103);
                graphics2.DrawImage(this.iDifficulty, 0, 0, 0x170, 0x103);
                this.nCurrentDrumspeed = CDTXMania.ConfigIni.n譜面スクロール速度.Drums;
                Brush namecolor = Brushes.White;
                LinearGradientBrush gb = new LinearGradientBrush( graphics.VisibleClipBounds, Color.LightYellow, Color.FromArgb(255, 245, 201), LinearGradientMode.Vertical );
                switch (CDTXMania.ConfigIni.nNameColor)
                {
                    case 0:
                        namecolor = Brushes.White;
                        break;
                    case 1:
                        namecolor = Brushes.LightYellow;
                        break;
                    case 2:
                        namecolor = Brushes.Yellow;
                        break;
                    case 3:
                        namecolor = Brushes.Green;
                        break;
                    case 4:
                        namecolor = Brushes.Blue;
                        break;
                    case 5:
                        namecolor = Brushes.Purple;
                        break;
                    case 6:
                        namecolor = Brushes.Red;
                        break;
                    case 7:
                        namecolor = Brushes.Brown;
                        break;
                    case 8:
                        namecolor = Brushes.Silver;
                        break;
                    case 9:
                        namecolor = Brushes.Gold;
                        break;

                    case 10:
                        namecolor = Brushes.White;
                        break;
                    case 11:
                        gb = new LinearGradientBrush( graphics.VisibleClipBounds, Color.LightYellow,             Color.FromArgb(255, 245, 201), LinearGradientMode.Vertical );
                        break;
                    case 12:
                        gb = new LinearGradientBrush( graphics.VisibleClipBounds, Color.FromArgb(255, 216, 0),   Color.FromArgb(255, 238, 153), LinearGradientMode.Vertical );
                        break;
                    case 13:
                        gb = new LinearGradientBrush( graphics.VisibleClipBounds, Color.FromArgb(0, 255, 33),    Color.FromArgb(153, 255, 164), LinearGradientMode.Vertical);
                        break;
                    case 14:
                        gb = new LinearGradientBrush( graphics.VisibleClipBounds, Color.FromArgb(0, 38, 255),    Color.FromArgb(178, 189, 255), LinearGradientMode.Vertical);
                        break;
                    case 15:
                        gb = new LinearGradientBrush( graphics.VisibleClipBounds, Color.FromArgb(72, 0, 255),    Color.FromArgb(180, 153, 255), LinearGradientMode.Vertical);
                        break;
                    case 16:
                        gb = new LinearGradientBrush( graphics.VisibleClipBounds, Color.FromArgb(255, 0, 0),     Color.FromArgb(255, 153, 153), LinearGradientMode.Vertical);
                        break;
                    case 17:
                        gb = new LinearGradientBrush( graphics.VisibleClipBounds, Color.FromArgb(232, 182, 149), Color.FromArgb(122, 69, 26), LinearGradientMode.Vertical);
                        break;
                    case 18:
                        gb = new LinearGradientBrush( graphics.VisibleClipBounds, Color.FromArgb(246, 245, 255), Color.FromArgb(125, 128, 137), LinearGradientMode.Vertical);
                        break;
                    case 19:
                        gb = new LinearGradientBrush( graphics.VisibleClipBounds, Color.FromArgb(255, 241, 181), Color.FromArgb(238, 196, 85), LinearGradientMode.Vertical);
                        break;
                }
                graphics.DrawImage(this.iDrumspeed, new Rectangle(209, 156, 42, 48), new Rectangle(0, ((this.nCurrentDrumspeed > 15) ? 15 : this.nCurrentDrumspeed) * 0x30, 0x2a, 0x30), GraphicsUnit.Pixel);
                graphics.DrawImage(this.iRisky, new Rectangle(260, 156, 42, 48), new Rectangle(0, ((CDTXMania.ConfigIni.nRisky > 10) ? 10 : CDTXMania.ConfigIni.nRisky) * 48, 42, 48), GraphicsUnit.Pixel);

                #region[ 名前、グループ名 ]
                if (this.nStrlengthbydot > 240)
                {
                    this.ftDisplayFont = new Font("ＤＦＧ平成ゴシック体W5", 16f, FontStyle.Regular, GraphicsUnit.Pixel);
                    graphics.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, (float)80f, (float)68f);
                    if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A || CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.C)
                    {
                        if (CDTXMania.ConfigIni.nNameColor >= 11)
                        {
                            graphics.DrawString(this.strPlayerName, this.ftNameFont, gb, (float)54f, (float)136f);
                        }
                        else
                        {
                            graphics.DrawString(this.strPlayerName, this.ftNameFont, namecolor, (float)54f, (float)136f);
                        }
                        graphics.DrawString(this.strGroupName, this.ftGroupFont, Brushes.White, (float)54f, (float)105f);
                    }
                    else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                    {
                        if (CDTXMania.ConfigIni.nNameColor >= 11)
                        {
                            graphics.DrawString(this.strPlayerName, this.ftNameFont, gb, (float)54f, (float)110f);
                        }
                        else
                        {
                            graphics.DrawString(this.strPlayerName, this.ftNameFont, namecolor, (float)54f, (float)110f);
                        }
                        graphics.DrawString(this.strGroupName, this.ftGroupFont, Brushes.White, (float)54f, (float)500f);
                    }
                }
                else
                {
                    graphics.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, (float)80f, (float)62f);
                    if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A || CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.C)
                    {
                        if (CDTXMania.ConfigIni.nNameColor >= 11)
                        {
                            graphics.DrawString(this.strPlayerName, this.ftNameFont, gb, (float)54f, (float)136f);
                        }
                        else
                        {
                            graphics.DrawString(this.strPlayerName, this.ftNameFont, namecolor, (float)54f, (float)136f);
                        }
                        graphics.DrawString(this.strGroupName, this.ftGroupFont, Brushes.White, (float)54f, (float)105f);
                    }
                    else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                    {
                        if (CDTXMania.ConfigIni.nNameColor >= 11)
                        {
                            graphics.DrawString(this.strPlayerName, this.ftNameFont, gb, (float)54f, (float)110f);
                        }
                        else
                        {
                            graphics.DrawString(this.strPlayerName, this.ftNameFont, namecolor, (float)54f, (float)110f);
                        }
                        graphics.DrawString(this.strGroupName, this.ftGroupFont, Brushes.White, (float)54f, (float)500f);
                    }
                }
                #endregion

                string str = string.Format("{0:0.00}", ((float)CDTXMania.DTX.LEVEL.Drums) / 10f); ;
                    if (CDTXMania.DTX.LEVEL.Drums > 100)
                    {
                        str = string.Format("{0:0.00}", ((float)CDTXMania.DTX.LEVEL.Drums) / 100f);
                    }
                    else
                    {
                        str = string.Format("{0:0.00}", ((float)CDTXMania.DTX.LEVEL.Drums) / 10f);
                    }
                
                if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && (CDTXMania.DTX.bチップがある.LeftCymbal == false) && (CDTXMania.DTX.bチップがある.LP == false) && (CDTXMania.DTX.bチップがある.LBD == false) && (CDTXMania.DTX.bチップがある.FT == false) && (CDTXMania.DTX.bチップがある.Ride == false))
                {
                    str = string.Format("{0:00}", ((float)CDTXMania.DTX.LEVEL.Drums));
                }
                

                int width = (int)graphics.MeasureString(this.stパネルマップ[this.nIndex].label.Substring(0, 3) + "   ", this.ftLevelFont).Width;
                
                //数字の描画部分。その左側。
                if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && (CDTXMania.DTX.bチップがある.LeftCymbal == false) && (CDTXMania.DTX.bチップがある.LP == false) && (CDTXMania.DTX.bチップがある.LBD == false) && (CDTXMania.DTX.bチップがある.FT == false) && (CDTXMania.DTX.bチップがある.Ride == false))
                {
                    if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                    graphics2.DrawString(str.Substring(0, 2), this.ftDifficultyL, Brushes.Black, (float)(18f + width), (float)164f);
                    else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                        graphics2.DrawString(str.Substring(0, 2), this.ftDifficultyL, Brushes.Black, (float)(24f + width), (float)168f);
                    else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.C)
                        graphics2.DrawString(str.Substring(0, 2), this.ftDifficultyL, Brushes.Black, (float)(18f + width), (float)164f);
                }
                else
                {
                    if(CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                    graphics2.DrawString(str.Substring(0, 1), this.ftDifficultyL, Brushes.Black, (float)(12f + width), (float)164f);

                    else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                    graphics2.DrawString(str.Substring(0, 1), this.ftDifficultyL, Brushes.Black, (float)(14f + width), (float)146f);

                    else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.C)
                    graphics2.DrawString(str.Substring(0, 1), this.ftDifficultyL, Brushes.Black, (float)(12f + width), (float)168f);
                }
                width += (int)graphics2.MeasureString(str.Substring(0, 1), this.ftDifficultyL).Width;

                //数字の右。
                if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && (CDTXMania.DTX.bチップがある.LeftCymbal == false) && (CDTXMania.DTX.bチップがある.LP == false) && (CDTXMania.DTX.bチップがある.LBD == false) && (CDTXMania.DTX.bチップがある.FT == false) && (CDTXMania.DTX.bチップがある.Ride == false))
                {

                }
                else
                {
                    if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                    graphics2.DrawString(str.Substring(1, 3), this.ftDifficultyS, Brushes.Black, (float)width, 176f);

                    else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                    graphics2.DrawString(str.Substring(1, 3), this.ftDifficultyS, Brushes.Black, (float)(2f + width), 180f);

                    else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.C)
                    graphics2.DrawString(str.Substring(1, 3), this.ftDifficultyS, Brushes.Black, (float)width, 180f);

                }

                //ジャケット画像描画部
                string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PREIMAGE;
                if (!File.Exists(path))
                {
                    this.iAlbum = Image.FromFile(CSkin.Path(@"Graphics\5_preimage default.png"));
                }
                else
                {
                    this.iAlbum = Image.FromFile(path);
                }
                graphics.DrawImage(this.iAlbum, new Rectangle(7, 0x11, 0x45, 0x4b), new Rectangle(0, 0, this.iAlbum.Width, this.iAlbum.Height), GraphicsUnit.Pixel);
                graphics.Dispose();
                graphics2.Dispose();
                //テクスチャ変換
                this.txNamePlate = new CTexture(CDTXMania.app.Device, this.bNamePlate, CDTXMania.TextureFormat, false);
                this.txDummy = new CTexture(CDTXMania.app.Device, this.bDifficulty, CDTXMania.TextureFormat, false);

                //
                this.iAlbum.Dispose();
                this.iNamePlate.Dispose();
                this.iDifficulty.Dispose();

                //ここで使用したフォント3つはここで開放。
                this.ftLevelFont.Dispose();
                this.ftDisplayFont.Dispose();
                this.ftNameFont.Dispose();

                base.OnManagedリソースの作成();
            }
        }
        public override void OnManagedリソースの解放()
        {
            if (!base.b活性化してない)
            {
                //テクスチャ5枚
                //イメージ 6枚(ジャケット画像はここで解放しない)
                //フォント 5個
                CDTXMania.tテクスチャの解放(ref this.txnameplate);
                CDTXMania.tテクスチャの解放(ref this.txNamePlate);
                CDTXMania.tテクスチャの解放(ref this.txDifficulty);
                CDTXMania.tテクスチャの解放(ref this.txDummy);
                CDTXMania.tテクスチャの解放(ref this.txScore);
                this.iNamePlate.Dispose();
                this.iDrumspeed.Dispose();
                this.iRisky.Dispose();
                this.iScore.Dispose();
                this.iDifficulty.Dispose();

                this.ftDifficultyS.Dispose();
                this.ftDifficultyL.Dispose();
                this.ftDisplayFont.Dispose();
                this.ftNameFont.Dispose();
                this.ftLevelFont.Dispose();
                base.OnManagedリソースの解放();
            }
        }

        public override int On進行描画()
        {
            if (!base.b活性化してない)
            {
                SlimDX.Matrix identity = SlimDX.Matrix.Identity;
                SlimDX.Matrix identity2 = SlimDX.Matrix.Identity;
                SlimDX.Matrix mat = SlimDX.Matrix.Identity;
                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                {
                    identity *= SlimDX.Matrix.Translation(-980, 148, 0);
                    identity *= SlimDX.Matrix.Scaling(0.39f, 0.72f, 1f);
                    identity *= SlimDX.Matrix.RotationY(-0.6f);
                    identity *= SlimDX.Matrix.RotationZ(-0.01f);
                    mat *= SlimDX.Matrix.Translation(-1060, 104, 0);
                    mat *= SlimDX.Matrix.Scaling(0.39f, 0.69f, 1.0f);
                    mat *= SlimDX.Matrix.RotationY(-0.6f);
                    mat *= SlimDX.Matrix.RotationZ(-0.01f);
                    //this.txScore.fZ軸中心回転 = -0.01f;
                }
                //XG2風。ぜんぜんそれっぽくないとか言わない。
                else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                {
                    identity *= SlimDX.Matrix.Translation(-890, 218, 0);
                    identity *= SlimDX.Matrix.Scaling(0.44f, 0.68f, 1.0f);
                    identity *= SlimDX.Matrix.RotationY(-0.45f);
                    identity *= SlimDX.Matrix.RotationZ(-0.01f);
                    mat *= SlimDX.Matrix.Translation(-948, 162, 0);
                    mat *= SlimDX.Matrix.Scaling(0.46f, 0.68f, 1.0f);
                    mat *= SlimDX.Matrix.RotationY(-0.45f);
                    mat *= SlimDX.Matrix.RotationZ(-0.01f);
                }
                else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.C)
                {
                    identity *= SlimDX.Matrix.Translation(-760, 190, 0);
                    identity *= SlimDX.Matrix.Scaling(0.55f, 0.85f, 1.0f);
                    identity *= SlimDX.Matrix.RotationY(-0.26f);
                    identity *= SlimDX.Matrix.RotationZ(-0.01f);
                    mat *= SlimDX.Matrix.Translation(-860, 130, 0);
                    mat *= SlimDX.Matrix.Scaling(0.55f, 0.85f, 1.0f);
                    mat *= SlimDX.Matrix.RotationY(-0.26f);
                    mat *= SlimDX.Matrix.RotationZ(-0.01f);
                    //this.txScore.fZ軸中心回転 = -0.01f;
                }

                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A || CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B || CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.C)
                {
                    this.txNamePlate.t3D描画(CDTXMania.app.Device, identity);
                    if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A || CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.C)
                    {
                        this.txDifficulty.t3D描画(CDTXMania.app.Device, mat, new Rectangle(0, 0 + (this.nStatus * 38), 146, 38));
                    }
                    else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                    {
                        this.txDifficulty.t3D描画(CDTXMania.app.Device, mat, new Rectangle(0, 0 + (this.nStatus * 64), 194, 64));
                    }
                    
                    this.txDummy.t3D描画(CDTXMania.app.Device, identity);
                }
                if (this.nCurrentDrumspeed != CDTXMania.ConfigIni.n譜面スクロール速度.Drums)
                {
                    Graphics graphics = Graphics.FromImage(this.bNamePlate);
                    this.nCurrentDrumspeed = CDTXMania.ConfigIni.n譜面スクロール速度.Drums;
                    graphics.DrawImage(this.iDrumspeed, new Rectangle(0xd1, 0x9c, 0x2a, 0x30), new Rectangle(0, ((this.nCurrentDrumspeed > 15) ? 15 : this.nCurrentDrumspeed) * 0x30, 0x2a, 0x30), GraphicsUnit.Pixel);
                    graphics.Dispose();
                    this.txNamePlate.Dispose();
                    this.txNamePlate = new CTexture(CDTXMania.app.Device, this.bNamePlate, CDTXMania.TextureFormat, false);
                }
                this.nCurrentScore = (long)CDTXMania.stage演奏ドラム画面.actScore.n現在表示中のスコア.Drums;
                if (CDTXMania.ConfigIni.nSkillMode == 0)
                {
                    string str = this.nCurrentScore.ToString("0000000000");
                    for (int i = 0; i < 10; i++)
                    {
                        Rectangle rectangle;
                        char ch = str[i];
                        if (ch.Equals(' '))
                        {
                            rectangle = new Rectangle(0, 0, 32, 36);
                        }
                        else
                        {
                            int num3 = int.Parse(str.Substring(i, 1));
                            if (num3 < 5)
                            {
                                rectangle = new Rectangle((num3 * 32), 0, 32, 36);
                            }
                            else
                            {
                                rectangle = new Rectangle((num3 * 32), 0, 32, 36);
                            }
                        }
                        if (this.txScore != null)
                        {
                            SlimDX.Matrix matScoreXG = SlimDX.Matrix.Identity;
                            if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                            {
                                matScoreXG *= SlimDX.Matrix.Translation(-1100 + (i * 30), 114, 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(0.39f, 0.68f, 1.0f);
                                matScoreXG *= SlimDX.Matrix.RotationY(-0.45f);
                                matScoreXG *= SlimDX.Matrix.RotationZ(-0.01f);
                                this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                            }
                            else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                            {
                                matScoreXG *= SlimDX.Matrix.Translation(-1180 + (i * 30), 46, 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(0.35f, 0.72f, 1f);
                                matScoreXG *= SlimDX.Matrix.RotationY(-0.6f);
                                matScoreXG *= SlimDX.Matrix.RotationZ(-0.01f);
                                this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                            }
                            else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.C)
                            {
                                matScoreXG *= SlimDX.Matrix.Translation(-970 + (i * 30), 84, 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(0.48f, 0.85f, 1.0f);
                                matScoreXG *= SlimDX.Matrix.RotationY(-0.26f);
                                matScoreXG *= SlimDX.Matrix.RotationZ(-0.01f);
                                this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                            }
                        }
                    }
                }
                else if (CDTXMania.ConfigIni.nSkillMode == 1)
                {
                    string str = this.nCurrentScore.ToString("0000000");
                    for (int i = 0; i < 7; i++)
                    {
                        Rectangle rectangle;
                        char ch = str[i];
                        if (ch.Equals(' '))
                        {
                            rectangle = new Rectangle(0, 0, 32, 36);
                        }
                        else
                        {
                            int num3 = int.Parse(str.Substring(i, 1));
                            if (num3 < 5)
                            {
                                rectangle = new Rectangle((num3 * 32), 0, 32, 36);
                            }
                            else
                            {
                                rectangle = new Rectangle((num3 * 32), 0, 32, 36);
                            }
                        }
                        if (this.txScore != null)
                        {
                            SlimDX.Matrix matScoreXG = SlimDX.Matrix.Identity;
                            if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                            {
                                matScoreXG *= SlimDX.Matrix.Translation(-774 + (i * 30), 114, 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(0.55f, 0.68f, 1.0f);
                                matScoreXG *= SlimDX.Matrix.RotationY(-0.45f);
                                matScoreXG *= SlimDX.Matrix.RotationZ(-0.01f);
                                this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                            }
                            else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                            {
                                matScoreXG *= SlimDX.Matrix.Translation(-874 + (i * 30), 46, 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(0.47f, 0.72f, 1f);
                                matScoreXG *= SlimDX.Matrix.RotationY(-0.6f);
                                matScoreXG *= SlimDX.Matrix.RotationZ(-0.01f);
                                this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                            }
                            else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.C)
                            {
                                matScoreXG *= SlimDX.Matrix.Translation(-655 + (i * 30), 84, 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(0.7f, 0.85f, 1.0f);
                                matScoreXG *= SlimDX.Matrix.RotationY(-0.26f);
                                matScoreXG *= SlimDX.Matrix.RotationZ(-0.01f);
                                this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                            }
                        }
                    }
                }
            }
            return 0;

        }


        // その他

        #region [ private ]
        //-----------------
        private Bitmap b4font;
        private Bitmap bNamePlate;
        private Bitmap bDifficulty;
        private Font ftDifficultyL;
        private Font ftDifficultyS;
        private Font ftDisplayFont;
        //private Font ftDisplayFont2;
        private Font ftNameFont;
        private Font ftGroupFont;
        private Font ftLevelFont;
        private Image iAlbum;
        private Image iDrumspeed;
        private Image iRisky;
        private Image iNamePlate;
        private Image iDifficulty;
        private Image iScore;
        private int nCurrentDrumspeed;
        private int nStrlengthbydot;
        private string strGroupName;
        private string strPanelString;
        private string strPlayerName;
        private CTexture txDummy;
        private CTexture txDifficulty;
        private CTexture txnameplate;
        private CTexture txNamePlate;
        private CTexture txScore;
        //-----------------
        #endregion
    }
}
