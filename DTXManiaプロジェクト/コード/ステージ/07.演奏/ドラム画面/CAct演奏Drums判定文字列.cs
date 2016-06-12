﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;
using FDK;
using SlimDX;

namespace DTXMania
{
	internal class CAct演奏Drums判定文字列 : CAct演奏判定文字列共通
	{
		// コンストラクタ

		public CAct演奏Drums判定文字列()
		{
			this.stレーンサイズ = new STレーンサイズ[ 12 ]
			{
				new STレーンサイズ( 290, 80 ),
				new STレーンサイズ( 367, 46 ),
				new STレーンサイズ( 470, 54 ),
				new STレーンサイズ( 582, 60 ),
				new STレーンサイズ( 528, 46 ),
				new STレーンサイズ( 645, 46 ),
				new STレーンサイズ( 694, 46 ),
				new STレーンサイズ( 748, 64 ),
				new STレーンサイズ( 419, 46 ),
				new STレーンサイズ( 815, 80 ),
				new STレーンサイズ( 815, 80 ),
				new STレーンサイズ( 815, 80 )
			};
			//for ( int i = 0; i < 12; i++ )
			//{
			//	this.stレーンサイズ[i] = new STレーンサイズ();
			//	this.stレーンサイズ[i].x = sizeXW[i, 0];
			//	this.stレーンサイズ[i].w = sizeXW[i, 1];
			//}
			base.b活性化してない = true;
		}
		
        /// <summary>
        /// レーンのX座標をint配列に格納していく。
        /// </summary>
        /// <param name="eLaneType">レーンタイプ</param>
        private void tレーンタイプからレーン位置を設定する( Eタイプ eLaneType, ERDPosition eRDPosition )
        {
            switch( eLaneType )
            {
                case Eタイプ.A:
                    this.stレーンサイズ[ 2 ] = new STレーンサイズ( 470, 54 );
                    this.stレーンサイズ[ 3 ] = new STレーンサイズ( 582, 60 );
                    this.stレーンサイズ[ 4 ] = new STレーンサイズ( 528, 46 );
                    this.stレーンサイズ[ 8 ] = new STレーンサイズ( 419, 46 );
                    break;
                case Eタイプ.B:
                    this.stレーンサイズ[ 2 ] = new STレーンサイズ( 419, 54 );
                    this.stレーンサイズ[ 3 ] = new STレーンサイズ( 534, 60 );
                    this.stレーンサイズ[ 4 ] = new STレーンサイズ( 590, 46 );
                    this.stレーンサイズ[ 8 ] = new STレーンサイズ( 478, 46 );
                    break;
                case Eタイプ.C:
                    this.stレーンサイズ[ 2 ] = new STレーンサイズ( 470, 54 );
                    this.stレーンサイズ[ 3 ] = new STレーンサイズ( 534, 60 );
                    this.stレーンサイズ[ 4 ] = new STレーンサイズ( 590, 46 );
                    this.stレーンサイズ[ 8 ] = new STレーンサイズ( 419, 46 );
                    break;
                case Eタイプ.D:
                    this.stレーンサイズ[ 2 ] = new STレーンサイズ( 419, 54 );
                    this.stレーンサイズ[ 3 ] = new STレーンサイズ( 582, 60 );
                    this.stレーンサイズ[ 4 ] = new STレーンサイズ( 476, 46 );
                    this.stレーンサイズ[ 8 ] = new STレーンサイズ( 528, 46 );
                    break;
            }

            if( eRDPosition == ERDPosition.RCRD )
            {
                this.stレーンサイズ[ 7 ] = new STレーンサイズ( 748, 64 );
                this.stレーンサイズ[ 9 ] = new STレーンサイズ( 815, 64 );
            }
            else
            {
                this.stレーンサイズ[ 7 ] = new STレーンサイズ( 818, 64 );
                this.stレーンサイズ[ 9 ] = new STレーンサイズ( 768, 64 );
            }
        }
		
		// CActivity 実装（共通クラスからの差分のみ）
		public override int On進行描画()
		{
			throw new InvalidOperationException( "t進行描画(C演奏判定ライン座標共通 演奏判定ライン共通 ) のほうを使用してください。" );
		}
		public override int t進行描画( C演奏判定ライン座標共通 演奏判定ライン座標 )
		{
            if( base.b初めての進行描画 )
            {
                this.tレーンタイプからレーン位置を設定する( CDTXMania.ConfigIni.eLaneType, CDTXMania.ConfigIni.eRDPosition );
            }

			if( !base.b活性化してない )
			{
                #region[ 座標など定義 ]
                if( CDTXMania.ConfigIni.eJudgeAnimeType == Eタイプ.B )
                {
                    #region[ コマ方式 ]
                    for (int i = 0; i < 12; i++)
                    {
                        if (!base.st状態[i].ct進行.b停止中)
                        {
                            base.st状態[i].ct進行.t進行();
                            if (base.st状態[i].ct進行.b終了値に達した)
                            {
                                base.st状態[i].ct進行.t停止();
                            }
                            base.st状態[i].nRect = base.st状態[i].ct進行.n現在の値;
                        }
                    }
                    #endregion
                }
                else if( CDTXMania.ConfigIni.eJudgeAnimeType == Eタイプ.C )
                {
                    #region[ 新しいやつ ]
                    for( int i = 0; i < 12; i++ )
                    {
                        if( !base.st状態[ i ].ct進行.b停止中 )
                        {
                            base.st状態[ i ].ct進行.t進行();
                            if( base.st状態[ i ].ct進行.b終了値に達した )
                            {
                                base.st状態[ i ].ct進行.t停止();
                            }
                            //int num2 = base.st状態[i].ct進行.n現在の値;
                            int nNowFrame = base.st状態[ i ].ct進行.n現在の値;
                            //CDTXMania.act文字コンソール.tPrint( 100, i * 16, C文字コンソール.Eフォント種別.白, base.st状態[ i ].ct進行.n現在の値.ToString() );
                            //テンプレのようなもの。
                            //拡大処理を先に行わないとめちゃくちゃになる。
                            /*
                            base.st状態[i].fX方向拡大率 = 1.0f;
                            base.st状態[i].fY方向拡大率 = 1.0f;
                            base.st状態[i].n相対X座標 = 0;
                            base.st状態[i].n相対Y座標 = 0;
                            base.st状態[i].n透明度 = 0;
                            */

                            //base.st状態[i].judge = E判定.Perfect;
                            //nNowFrame = 0;
                            if( base.st状態[ i ].judge == E判定.Perfect || base.st状態[ i ].judge == E判定.XPerfect )
                            {
                                #region[ PERFECT ]
                                #region[ 0～10 ]
                                if( nNowFrame == 0 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.67f;
                                    base.st状態[i].fY方向拡大率 = 1.67f;

                                    base.st状態[i].fZ軸回転度 = 0;
                                    //base.st状態[i].fX方向拡大率 = 1f;
                                    //base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].n相対X座標 = 28;
                                    base.st状態[i].n相対Y座標 = 0;
                                    base.st状態[i].n透明度 = 0;
                                    
                                    base.st状態[i].fX方向拡大率_棒 = 0f;
                                    base.st状態[i].fY方向拡大率_棒 = 0f;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian( -43f );
                                }
                                else if( nNowFrame == 1 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.33f;
                                    base.st状態[i].fY方向拡大率 = 1.33f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 7f );
                                    base.st状態[i].n相対X座標 = 26;
                                    base.st状態[i].n相対Y座標 = 4;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 0.63f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -98;
                                    base.st状態[i].n相対Y座標_棒 = 6;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian( -43f );
                                }
                                else if( nNowFrame == 2 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率B = 1f;
                                    base.st状態[i].fY方向拡大率B = 1f;
                                    base.st状態[i].n相対X座標B = -2;
                                    base.st状態[i].n相対Y座標B = 2;
                                    base.st状態[i].n透明度B = 127;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -40;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(-14.5f);
                                }
                                else if( nNowFrame == 3 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率B = 1.1f;
                                    base.st状態[i].fY方向拡大率B = 1.1f;
                                    base.st状態[i].n相対X座標B = -3;
                                    base.st状態[i].n相対Y座標B = 1;
                                    base.st状態[i].n透明度B = 127;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -40;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(15f);
                                }
                                else if( nNowFrame == 4 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率B = 1.2f;
                                    base.st状態[i].fY方向拡大率B = 1.2f;
                                    base.st状態[i].n相対X座標B = -4;
                                    base.st状態[i].n相対Y座標B = 0;
                                    base.st状態[i].n透明度B = 127;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -40;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(18.5f);
                                }
                                else if( nNowFrame == 5 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率B = 1.25f;
                                    base.st状態[i].fY方向拡大率B = 1.25f;
                                    base.st状態[i].n相対X座標B = -5;
                                    base.st状態[i].n相対Y座標B = -1;
                                    base.st状態[i].n透明度B = 127;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -38;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(20.5f);
                                }
                                else if( nNowFrame == 6 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率B = 1.3f;
                                    base.st状態[i].fY方向拡大率B = 1.3f;
                                    base.st状態[i].n相対X座標B = -6;
                                    base.st状態[i].n相対Y座標B = -2;
                                    base.st状態[i].n透明度B = 127;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -38;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(20.5f);
                                }
                                else if( nNowFrame == 7 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率B = 1.35f;
                                    base.st状態[i].fY方向拡大率B = 1.35f;
                                    base.st状態[i].n相対X座標B = -7;
                                    base.st状態[i].n相対Y座標B = -3;
                                    base.st状態[i].n透明度B = 127;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -39;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(22f);
                                }
                                else if( nNowFrame == 8 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率B = 1.4f;
                                    base.st状態[i].fY方向拡大率B = 1.4f;
                                    base.st状態[i].n相対X座標B = -8;
                                    base.st状態[i].n相対Y座標B = -4;
                                    base.st状態[i].n透明度B = 127;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -40;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(23.5f);
                                }
                                else if( nNowFrame == 9 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率B = 1.45f;
                                    base.st状態[i].fY方向拡大率B = 1.45f;
                                    base.st状態[i].n相対X座標B = -9;
                                    base.st状態[i].n相対Y座標B = -5;
                                    base.st状態[i].n透明度B = 112;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -40;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(25.5f);
                                }
                                else if( nNowFrame == 10 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;


                                    base.st状態[i].fX方向拡大率B = 1.5f;
                                    base.st状態[i].fY方向拡大率B = 1.5f;
                                    base.st状態[i].n相対X座標B = -10;
                                    base.st状態[i].n相対Y座標B = -6;
                                    base.st状態[i].n透明度B = 100;


                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -40;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(27f);
                                }
                                #endregion
                                #region[ 11～18 ]
                                else if( nNowFrame == 11 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率B = 1.55f;
                                    base.st状態[i].fY方向拡大率B = 1.55f;
                                    base.st状態[i].n相対X座標B = -11;
                                    base.st状態[i].n相対Y座標B = -7;
                                    base.st状態[i].n透明度B = 70;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -40;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(29.5f);
                                }
                                else if( nNowFrame == 12 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率B = 1.6f;
                                    base.st状態[i].fY方向拡大率B = 1.6f;
                                    base.st状態[i].n相対X座標B = -12;
                                    base.st状態[i].n相対Y座標B = -8;
                                    base.st状態[i].n透明度B = 40;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -38;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(31f);
                                }
                                else if( nNowFrame == 13 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率B = 1.65f;
                                    base.st状態[i].fY方向拡大率B = 1.65f;
                                    base.st状態[i].n相対X座標B = -13;
                                    base.st状態[i].n相対Y座標B = -9;
                                    base.st状態[i].n透明度B = 40;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -38;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(32.5f);
                                }
                                else if( nNowFrame == 14 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率B = 1.7f;
                                    base.st状態[i].fY方向拡大率B = 1.7f;
                                    base.st状態[i].n相対X座標B = -14;
                                    base.st状態[i].n相対Y座標B = -10;
                                    base.st状態[i].n透明度B = 20;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -38;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(34f);
                                }
                                else if( nNowFrame == 15 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率B = 1f;
                                    base.st状態[i].fY方向拡大率B = 1f;
                                    base.st状態[i].n相対X座標B = -14;
                                    base.st状態[i].n相対Y座標B = -10;
                                    base.st状態[i].n透明度B = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -38;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(36f);
                                }
                                else if( nNowFrame == 16 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -38;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(38f);
                                }
                                else if( nNowFrame == 17 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -46;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(40.5f);
                                }
                                else if( nNowFrame == 18 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -46;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(43f);
                                }
                                #endregion
                                #region[ 19～23 ]
                                else if( nNowFrame == 19 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.22f;
                                    base.st状態[i].fY方向拡大率 = 0.77f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = 16;
                                    base.st状態[i].n相対Y座標 = -2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.1f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -55;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(43f);
                                }
                                else if( nNowFrame == 20 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.45f;
                                    base.st状態[i].fY方向拡大率 = 0.64f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = 36;
                                    base.st状態[i].n相対Y座標 = -6;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 0.9f;
                                    base.st状態[i].fY方向拡大率_棒 = 0.7f;
                                    base.st状態[i].n相対X座標_棒 = -70;
                                    base.st状態[i].n相対Y座標_棒 = 4;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(43f);
                                }
                                else if( nNowFrame == 21 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.70f;
                                    base.st状態[i].fY方向拡大率 = 0.41f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = 57;
                                    base.st状態[i].n相対Y座標 = -9;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 0.6f;
                                    base.st状態[i].fY方向拡大率_棒 = 0.45f;
                                    base.st状態[i].n相対X座標_棒 = -98;
                                    base.st状態[i].n相対Y座標_棒 = 2;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(43f);
                                }
                                else if( nNowFrame == 22 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.91f;
                                    base.st状態[i].fY方向拡大率 = 0.23f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = 75;
                                    base.st状態[i].n相対Y座標 = -12;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 0.4f;
                                    base.st状態[i].fY方向拡大率_棒 = 0.25f;
                                    base.st状態[i].n相対X座標_棒 = -120;
                                    base.st状態[i].n相対Y座標_棒 = 2;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(43f);
                                }
                                else if( nNowFrame == 23 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.91f;
                                    base.st状態[i].fY方向拡大率 = 0.23f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( 15f );
                                    base.st状態[i].n相対X座標 = 75;
                                    base.st状態[i].n相対Y座標 = -12;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 0f;
                                    base.st状態[i].fY方向拡大率_棒 = 0f;
                                    base.st状態[i].n相対X座標_棒 = -120;
                                    base.st状態[i].n相対Y座標_棒 = 2;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(43f);
                                }
                                #endregion
                                #endregion
                            }
                            else if( base.st状態[ i ].judge == E判定.Great )
                            {
                                #region[ GREAT ]
                                #region[ 0～10 ]
                                if (nNowFrame == 0)
                                {
                                    base.st状態[i].fX方向拡大率 = 1.67f;
                                    base.st状態[i].fY方向拡大率 = 1.67f;

                                    base.st状態[i].fZ軸回転度 = 0;
                                    //base.st状態[i].fX方向拡大率 = 1f;
                                    //base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].n相対X座標 = 28;
                                    base.st状態[i].n相対Y座標 = 0;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 0f;
                                    base.st状態[i].fY方向拡大率_棒 = 0f;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(-43f);
                                }
                                else if (nNowFrame == 1)
                                {
                                    base.st状態[i].fX方向拡大率 = 1.33f;
                                    base.st状態[i].fY方向拡大率 = 1.33f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(7f);
                                    base.st状態[i].n相対X座標 = 26;
                                    base.st状態[i].n相対Y座標 = 4;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 0.63f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -98;
                                    base.st状態[i].n相対Y座標_棒 = 6;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(-43f);
                                }
                                else if (nNowFrame == 2)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -40;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(-14.5f);
                                }
                                else if (nNowFrame == 3)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -40;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(15f);
                                }
                                else if (nNowFrame == 4)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -40;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(18.5f);
                                }
                                else if (nNowFrame == 5)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -38;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(20.5f);
                                }
                                else if (nNowFrame == 6)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -38;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(20.5f);
                                }
                                else if (nNowFrame == 7)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -39;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(22f);
                                }
                                else if (nNowFrame == 8)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -40;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(23.5f);
                                }
                                else if (nNowFrame == 9)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -40;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(25.5f);
                                }
                                else if (nNowFrame == 10)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -40;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(27f);
                                }
                                #endregion
                                #region[ 11～18 ]
                                else if (nNowFrame == 11)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -40;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(29.5f);
                                }
                                else if (nNowFrame == 12)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -38;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(31f);
                                }
                                else if (nNowFrame == 13)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -38;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(32.5f);
                                }
                                else if (nNowFrame == 14)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -38;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(34f);
                                }
                                else if (nNowFrame == 15)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -38;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(36f);
                                }
                                else if (nNowFrame == 16)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -38;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(38f);
                                }
                                else if (nNowFrame == 17)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -46;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(40.5f);
                                }
                                else if (nNowFrame == 18)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = -2;
                                    base.st状態[i].n相対Y座標 = 2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.25f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -46;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(43f);
                                }
                                #endregion
                                #region[ 19～23 ]
                                else if (nNowFrame == 19)
                                {
                                    base.st状態[i].fX方向拡大率 = 1.22f;
                                    base.st状態[i].fY方向拡大率 = 0.77f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = 16;
                                    base.st状態[i].n相対Y座標 = -2;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 1.1f;
                                    base.st状態[i].fY方向拡大率_棒 = 1f;
                                    base.st状態[i].n相対X座標_棒 = -55;
                                    base.st状態[i].n相対Y座標_棒 = 10;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(43f);
                                }
                                else if (nNowFrame == 20)
                                {
                                    base.st状態[i].fX方向拡大率 = 1.45f;
                                    base.st状態[i].fY方向拡大率 = 0.64f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = 36;
                                    base.st状態[i].n相対Y座標 = -6;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 0.9f;
                                    base.st状態[i].fY方向拡大率_棒 = 0.7f;
                                    base.st状態[i].n相対X座標_棒 = -70;
                                    base.st状態[i].n相対Y座標_棒 = 4;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(43f);
                                }
                                else if (nNowFrame == 21)
                                {
                                    base.st状態[i].fX方向拡大率 = 1.70f;
                                    base.st状態[i].fY方向拡大率 = 0.41f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = 57;
                                    base.st状態[i].n相対Y座標 = -9;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 0.6f;
                                    base.st状態[i].fY方向拡大率_棒 = 0.45f;
                                    base.st状態[i].n相対X座標_棒 = -98;
                                    base.st状態[i].n相対Y座標_棒 = 2;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(43f);
                                }
                                else if (nNowFrame == 22)
                                {
                                    base.st状態[i].fX方向拡大率 = 1.91f;
                                    base.st状態[i].fY方向拡大率 = 0.23f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = 75;
                                    base.st状態[i].n相対Y座標 = -12;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 0.4f;
                                    base.st状態[i].fY方向拡大率_棒 = 0.25f;
                                    base.st状態[i].n相対X座標_棒 = -120;
                                    base.st状態[i].n相対Y座標_棒 = 2;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(43f);
                                }
                                else if (nNowFrame == 23)
                                {
                                    base.st状態[i].fX方向拡大率 = 1.91f;
                                    base.st状態[i].fY方向拡大率 = 0.23f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian(15f);
                                    base.st状態[i].n相対X座標 = 75;
                                    base.st状態[i].n相対Y座標 = -12;
                                    base.st状態[i].n透明度 = 0;

                                    base.st状態[i].fX方向拡大率_棒 = 0f;
                                    base.st状態[i].fY方向拡大率_棒 = 0f;
                                    base.st状態[i].n相対X座標_棒 = -120;
                                    base.st状態[i].n相対Y座標_棒 = 2;
                                    base.st状態[i].fZ軸回転度_棒 = C変換.DegreeToRadian(43f);
                                }
                                #endregion
                                #endregion
                            }
                            else if( base.st状態[ i ].judge == E判定.Good )
                            {
                                #region[ GOOD ]
                                if( nNowFrame == 0 )
                                {
                                    base.st状態[i].fX方向拡大率 = 0.625f;
                                    base.st状態[i].fY方向拡大率 = 3.70f;
                                    base.st状態[i].n相対X座標 = -19;
                                    base.st状態[i].n相対Y座標 = 0;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame == 1 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.125f;
                                    base.st状態[i].fY方向拡大率 = 2.00f;
                                    base.st状態[i].n相対X座標 = 4;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame == 2 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.375f;
                                    base.st状態[i].fY方向拡大率 = 0.66f;
                                    base.st状態[i].n相対X座標 = 13;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame == 3 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.25f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].n相対X座標 = 8;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame >= 4 && nNowFrame <= 18 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame == 19 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.25f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].n相対X座標 = 8;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame == 20 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.375f;
                                    base.st状態[i].fY方向拡大率 = 0.66f;
                                    base.st状態[i].n相対X座標 = 13;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame == 21 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.50f;
                                    base.st状態[i].fY方向拡大率 = 0.50f;
                                    base.st状態[i].n相対X座標 = 20;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame == 22 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.91f;
                                    base.st状態[i].fY方向拡大率 = 0.23f;
                                    base.st状態[i].n相対X座標 = 37;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame == 23 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.91f;
                                    base.st状態[i].fY方向拡大率 = 0.23f;
                                    base.st状態[i].n相対X座標 = 37;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                #endregion
                            }
                            else if( base.st状態[ i ].judge == E判定.Poor || base.st状態[ i ].judge == E判定.Miss )
                            {
                                #region[ POOR & MISS ]
                                if( nNowFrame == 0 )
                                {
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = -18;
                                    base.st状態[i].n透明度 = 100;
                                }
                                else if( nNowFrame == 1 )
                                {
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = -12;
                                    base.st状態[i].n透明度 = 140;
                                }
                                else if( nNowFrame == 2 )
                                {
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = -6;
                                    base.st状態[i].n透明度 = 190;
                                }
                                else if( nNowFrame == 3 )
                                {
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = 0;
                                    base.st状態[i].n透明度 = 220;
                                }
                                else if( nNowFrame == 4 )
                                {
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = -4;
                                    base.st状態[i].n透明度 = 255;
                                }
                                else if( nNowFrame == 5 )
                                {
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = -6;
                                    base.st状態[i].n透明度 = 255;
                                }
                                else if( nNowFrame >= 6 && nNowFrame <= 18 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = 0;
                                    base.st状態[i].n透明度 = 255;
                                }
                                else if( nNowFrame == 19 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( -4f );
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = 0;
                                    base.st状態[i].n透明度 = 220;
                                }
                                else if( nNowFrame == 20 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( -8f );
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = 6;
                                    base.st状態[i].n透明度 = 190;
                                }
                                else if( nNowFrame == 21 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( -8f );
                                    base.st状態[i].n相対X座標 = 20;
                                    base.st状態[i].n相対Y座標 = 12;
                                    base.st状態[i].n透明度 = 140;
                                }
                                else if( nNowFrame == 22 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( -12f );
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = 18;
                                    base.st状態[i].n透明度 = 100;
                                }
                                else if( nNowFrame == 23 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].fZ軸回転度 = C変換.DegreeToRadian( -16f );
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = 24;
                                    base.st状態[i].n透明度 = 70;
                                }
                                #endregion
                            }
                            else if( base.st状態[ i ].judge == E判定.Auto )
                            {
                                #region[ Auto ]
                                if( nNowFrame == 0 )
                                {
                                    base.st状態[i].fX方向拡大率 = 0.625f;
                                    base.st状態[i].fY方向拡大率 = 3.70f;
                                    base.st状態[i].n相対X座標 = -19;
                                    base.st状態[i].n相対Y座標 = 0;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame == 1 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.125f;
                                    base.st状態[i].fY方向拡大率 = 2.00f;
                                    base.st状態[i].n相対X座標 = 4;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame == 2 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.375f;
                                    base.st状態[i].fY方向拡大率 = 0.66f;
                                    base.st状態[i].n相対X座標 = 13;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame == 3 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.25f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].n相対X座標 = 8;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame >= 4 && nNowFrame <= 18 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame == 19 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.25f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].n相対X座標 = 8;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame == 20 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.375f;
                                    base.st状態[i].fY方向拡大率 = 0.66f;
                                    base.st状態[i].n相対X座標 = 13;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame == 21 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.50f;
                                    base.st状態[i].fY方向拡大率 = 0.50f;
                                    base.st状態[i].n相対X座標 = 20;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame == 22 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.91f;
                                    base.st状態[i].fY方向拡大率 = 0.23f;
                                    base.st状態[i].n相対X座標 = 37;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                else if( nNowFrame == 23 )
                                {
                                    base.st状態[i].fX方向拡大率 = 1.91f;
                                    base.st状態[i].fY方向拡大率 = 0.23f;
                                    base.st状態[i].n相対X座標 = 37;
                                    base.st状態[i].n相対Y座標 = 1;
                                    base.st状態[i].n透明度 = 0;
                                }
                                #endregion
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region[ むかしの ]
                    for (int i = 0; i < 12; i++)
                    {
                        if (!base.st状態[i].ct進行.b停止中)
                        {
                            base.st状態[i].ct進行.t進行();
                            if (base.st状態[i].ct進行.b終了値に達した)
                            {
                                base.st状態[i].ct進行.t停止();
                            }
                            int num2 = base.st状態[i].ct進行.n現在の値;
                            if ((base.st状態[i].judge != E判定.Miss) && (base.st状態[i].judge != E判定.Bad))
                            {
                                if (num2 < 50)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f + (1f * (1f - (((float)num2) / 50f)));
                                    base.st状態[i].fY方向拡大率 = ((float)num2) / 50f;
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = 0;
                                    base.st状態[i].n透明度 = 0xff;
                                }
                                else if (num2 < 130)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = ((num2 % 6) == 0) ? (CDTXMania.Random.Next(6) - 3) : base.st状態[i].n相対Y座標;
                                    base.st状態[i].n透明度 = 0xff;
                                }
                                else if (num2 >= 240)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f - ((1f * (num2 - 240)) / 60f);
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = 0;
                                    base.st状態[i].n透明度 = 0xff;
                                }
                                else
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = 0;
                                    base.st状態[i].n透明度 = 0xff;
                                }
                            }
                            else if (num2 < 50)
                            {
                                base.st状態[i].fX方向拡大率 = 1f;
                                base.st状態[i].fY方向拡大率 = ((float)num2) / 50f;
                                base.st状態[i].n相対X座標 = 0;
                                base.st状態[i].n相対Y座標 = 0;
                                base.st状態[i].n透明度 = 0xff;
                            }
                            else if (num2 >= 200)
                            {
                                base.st状態[i].fX方向拡大率 = 1f - (((float)(num2 - 200)) / 100f);
                                base.st状態[i].fY方向拡大率 = 1f - (((float)(num2 - 200)) / 100f);
                                base.st状態[i].n相対X座標 = 0;
                                base.st状態[i].n相対Y座標 = 0;
                                base.st状態[i].n透明度 = 0xff;
                            }
                            else
                            {
                                base.st状態[i].fX方向拡大率 = 1f;
                                base.st状態[i].fY方向拡大率 = 1f;
                                base.st状態[i].n相対X座標 = 0;
                                base.st状態[i].n相対Y座標 = 0;
                                base.st状態[i].n透明度 = 0xff;
                            }
                        }
                    }
                    #endregion
                }
                #endregion

                for( int j = 0; j < 12; j++ )
                {
                    //CDTXMania.act文字コンソール.tPrint( this.stレーンサイズ[ j ].x, 0, C文字コンソール.Eフォント種別.白, j.ToString() );
                    if (!base.st状態[j].ct進行.b停止中)
                    {
                        #region[ 以前まで ]
                        // 2016.02.16 kairera0467 104の仕様にあわせて従来のコードに加筆修正。
                        //                        現時点ではドラム画面でのギタープレイはできないため、この辺は適当。
                        if( CDTXMania.ConfigIni.eJudgeAnimeType != Eタイプ.C )
                        {
                            int num4 = CDTXMania.ConfigIni.nJudgeFrames > 1 ? 0 : base.st判定文字列[(int)base.st状態[j].judge].n画像番号;
                            int num5 = 0;
                            int num6 = 0;
                            int nJudgePosY = CDTXMania.stage演奏ドラム画面.演奏判定ライン座標.n判定ラインY座標( E楽器パート.DRUMS, false, CDTXMania.ConfigIni.bReverse.Drums, false, true );
                            base.iP_A = nJudgePosY - 211;
                            base.iP_B = nJudgePosY + 23; //これは固定値にしてもいいのではないか?
                            if( j < 10 )
                            {
                                //縦は5pxずつを1ブロックとして分割。これでだいぶ本家に近づいたはず。
                                if( CDTXMania.ConfigIni.判定文字表示位置.Drums == E判定文字表示位置.表示OFF )
                                    continue;

                                num5 = this.stレーンサイズ[ j ].x;
                                if( CDTXMania.ConfigIni.判定文字表示位置.Drums == E判定文字表示位置.レーン上 )
                                    num6 = CDTXMania.ConfigIni.bReverse.Drums ? ( ( nJudgePosY + 211 ) - this.n文字の縦表示位置[ j ] * 5 ) : ( ( nJudgePosY - 211 ) + this.n文字の縦表示位置[ j ] * 5 );
                                else if( CDTXMania.ConfigIni.判定文字表示位置.Drums == E判定文字表示位置.判定ライン上 )
                                    num6 = 0;
                            }

                            int nRectX = CDTXMania.ConfigIni.nJudgeWidgh;
                            int nRectY = CDTXMania.ConfigIni.nJudgeHeight;

                            int xc = (num5 + base.st状態[j].n相対X座標) + (this.stレーンサイズ[j].w / 2);
                            int x = (xc - ((int)((110f * base.st状態[j].fX方向拡大率)))) - ((nRectX - 225) / 2);
                            int y = ((num6 + base.st状態[j].n相対Y座標) - ((int)(((140f * base.st状態[j].fY方向拡大率)) / 2.0))) - ((nRectY - 135) / 2);

                            if( base.tx判定文字列[ 0 ] != null )
                            {
                                //if (CDTXMania.ConfigIni.nJudgeFrames > 1 && CDTXMania.stage演奏ドラム画面.tx判定画像anime != null)
                                if( CDTXMania.ConfigIni.nJudgeFrames > 1 )
                                {
                                    if( base.st状態[j].judge == E判定.Perfect || base.st状態[ j ].judge == E判定.XPerfect )
                                    {
                                        base.tx判定文字列[ 0 ].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                        //CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    }
                                    if (base.st状態[j].judge == E判定.Great)
                                    {
                                        base.tx判定文字列[ 0 ].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 1, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                        //CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 1, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    }
                                    if (base.st状態[j].judge == E判定.Good)
                                    {
                                        base.tx判定文字列[ 0 ].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 2, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                        //CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 2, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    }
                                    if (base.st状態[j].judge == E判定.Poor)
                                    {
                                        base.tx判定文字列[ 0 ].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 3, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                        //CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 3, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    }
                                    if( base.st状態[ j ].judge == E判定.Miss )
                                    {
                                        base.tx判定文字列[ 0 ].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 4, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                        //CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 4, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    }
                                    if (base.st状態[j].judge == E判定.Auto)
                                    {
                                        base.tx判定文字列[ 0 ].t2D描画( CDTXMania.app.Device, x, y, new Rectangle( nRectX * 5, nRectY * base.st状態[ j ].nRect, nRectX, nRectY ) );
                                        //CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 5, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    }
                                }
                                else if (base.tx判定文字列[num4] != null)
                                {
                                    x = xc - ((int)((64f * base.st状態[j].fX方向拡大率)));
                                    y = (num6 + base.st状態[j].n相対Y座標) - ((int)(((43f * base.st状態[j].fY方向拡大率)) / 2.0));

                                    base.tx判定文字列[num4].n透明度 = base.st状態[j].n透明度;
                                    base.tx判定文字列[num4].vc拡大縮小倍率 = new Vector3(base.st状態[j].fX方向拡大率, base.st状態[j].fY方向拡大率, 1f);
                                    base.tx判定文字列[num4].t2D描画(CDTXMania.app.Device, x, y, base.st判定文字列[(int)base.st状態[j].judge].rc);
                                }


                                if (base.nShowLagType == (int)EShowLagType.ON ||
                                     ((base.nShowLagType == (int)EShowLagType.GREAT_POOR) && (base.st状態[j].judge != E判定.Perfect)))
                                {
                                    if (base.st状態[j].judge != E判定.Auto && base.txlag数値 != null)		// #25370 2011.2.1 yyagi
                                    {
                                        bool minus = false;
                                        int offsetX = 0;
                                        string strDispLag = base.st状態[j].nLag.ToString();
                                        if (st状態[j].nLag < 0)
                                        {
                                            minus = true;
                                        }
                                        x = xc - strDispLag.Length * 15 / 2;
                                        for (int i = 0; i < strDispLag.Length; i++)
                                        {
                                            int p = (strDispLag[i] == '-') ? 11 : (int)(strDispLag[i] - '0');	//int.Parse(strDispLag[i]);
                                            p += minus ? 0 : 12;		// change color if it is minus value
                                            base.txlag数値.t2D描画(CDTXMania.app.Device, x + offsetX, y + 34, base.stLag数値[p].rc);
                                            offsetX += 15;
                                        }
                                    }
                                }

                            }
                        }
                        #endregion
                        #region[ さいしんばん ]
                        else if( CDTXMania.ConfigIni.eJudgeAnimeType == Eタイプ.C )
                        {
                            int num4 = 0;
                            int num5 = 0;
                            int num6 = 0;
                            int nJudgePosY = CDTXMania.stage演奏ドラム画面.演奏判定ライン座標.n判定ラインY座標( E楽器パート.DRUMS, false, CDTXMania.ConfigIni.bReverse.Drums, false, true );
                            base.iP_A = nJudgePosY - 211;
                            base.iP_B = nJudgePosY + 23; //これは固定値にしてもいいのではないか?
                            if( j < 10 )
                            {
                                //縦は5pxずつを1ブロックとして分割。これでだいぶ本家に近づいたはず。
                                if( CDTXMania.ConfigIni.判定文字表示位置.Drums == E判定文字表示位置.表示OFF )
                                    continue;

                                num5 = this.stレーンサイズ[ j ].x;
                                if( CDTXMania.ConfigIni.判定文字表示位置.Drums == E判定文字表示位置.レーン上 )
                                    num6 = CDTXMania.ConfigIni.bReverse.Drums ? ( ( nJudgePosY + 211 ) - this.n文字の縦表示位置[ j ] * 5 ) : ( ( nJudgePosY - 211 ) + this.n文字の縦表示位置[ j ] * 5 );
                                else if( CDTXMania.ConfigIni.判定文字表示位置.Drums == E判定文字表示位置.判定ライン上 )
                                    num6 = CDTXMania.ConfigIni.bReverse.Drums ? ( 80 + this.n文字の縦表示位置[ j ] * 2 ) : ( 583 + this.n文字の縦表示位置[ j ] * 2 );
                            }

                            int nRectX = 85;
                            int nRectY = 35;

                            int xc = ( num5 + ( this.stレーンサイズ[ j ].w / 2 ) ) + base.st状態[ j ].n相対X座標;
                            int yc = ( num6 + base.st状態[ j ].n相対Y座標 ) + ( num6 / 2 );
                            float fRot = base.st状態[j].fZ軸回転度;
                            int x = ( xc - ((int)(((nRectX * base.st状態[j].fX方向拡大率 ) / base.st状態[j].fX方向拡大率) * base.st状態[j].fX方向拡大率)) + (nRectX / 2));
                            int y = ( num6 + base.st状態[j].n相対Y座標 ) - ((int)((((nRectY) / 2) * base.st状態[j].fY方向拡大率)));

                            int xc_棒 = ( num5 + base.st状態[j].n相対X座標_棒) + (this.stレーンサイズ[j].w / 2);
                            int yc_棒 = ( num6 + base.st状態[j].n相対Y座標_棒) + (num6 / 2);
                            float fRot_棒 = base.st状態[j].fZ軸回転度_棒;
                            int x_棒 = ( xc_棒 - ((int)(((nRectX * base.st状態[j].fX方向拡大率_棒) / base.st状態[j].fX方向拡大率_棒) * base.st状態[j].fX方向拡大率_棒)) + (nRectX / 2));
                            int y_棒 = ( num6 + base.st状態[ j ].n相対Y座標_棒 ) - ( (int)( ( ( ( nRectY ) / 2 ) * base.st状態[j].fY方向拡大率_棒 ) ) );

                            //2016.06.09 kairera0467 棒の高さを30pxにするテスト
                            int n棒高さ = 20;


                            //CDTXMania.act文字コンソール.tPrint( 0, j * 16, C文字コンソール.Eフォント種別.白, x.ToString() );
                            if( base.tx判定文字列[ 0 ] != null )
                            {
                                switch( base.st状態[ j ].judge )
                                {
                                    case E判定.XPerfect:
                                        {
                                            base.tx判定文字列[ 1 ].vc拡大縮小倍率 = new Vector3( base.st状態[ j ].fX方向拡大率_棒, base.st状態[ j ].fY方向拡大率_棒, 1f );
                                            base.tx判定文字列[ 1 ].fZ軸中心回転 = base.st状態[ j ].fZ軸回転度_棒;
                                            //base.tx判定文字列[ 1 ].t2D描画( CDTXMania.app.Device, x_棒, y_棒, new Rectangle( 0, 140, 210, 15 ) );
                                            base.tx判定文字列[ 1 ].t2D描画( CDTXMania.app.Device, x_棒, y_棒, new Rectangle( 0, 110 + n棒高さ * 2, 210, n棒高さ ) );

                                            base.tx判定文字列[ 0 ].vc拡大縮小倍率 = new Vector3(base.st状態[ j ].fX方向拡大率, base.st状態[ j ].fY方向拡大率, 1f );
                                            base.tx判定文字列[ 0 ].fZ軸中心回転 = base.st状態[ j ].fZ軸回転度;
                                            base.tx判定文字列[ 0 ].n透明度 = 255;
                                            base.tx判定文字列[ 0 ].t2D描画( CDTXMania.app.Device, x, y, new Rectangle( nRectX + 5, nRectY * 2 + 4, nRectX, nRectY ) );

                                            base.tx判定文字列[ 2 ].vc拡大縮小倍率 = new Vector3( base.st状態[ j ].fX方向拡大率B, base.st状態[ j ].fY方向拡大率B, 1f );
                                            base.tx判定文字列[ 2 ].fZ軸中心回転 = base.st状態[ j ].fZ軸回転度;
                                            base.tx判定文字列[ 2 ].n透明度 = base.st状態[ j ].n透明度B;
                                            base.tx判定文字列[ 2 ].b加算合成 = true;
                                            base.tx判定文字列[ 2 ].t2D描画( CDTXMania.app.Device, x, y, new Rectangle( nRectX + 5, nRectY * 2 + 4, nRectX, nRectY ) );
                                        }
                                        break;
                                    case E判定.Perfect:
                                        {
                                            base.tx判定文字列[ 1 ].vc拡大縮小倍率 = new Vector3( base.st状態[ j ].fX方向拡大率_棒, base.st状態[ j ].fY方向拡大率_棒, 1f );
                                            base.tx判定文字列[ 1 ].fZ軸中心回転 = base.st状態[ j ].fZ軸回転度_棒;
                                            //base.tx判定文字列[ 1 ].t2D描画( CDTXMania.app.Device, x_棒, y_棒, new Rectangle( 0, 110, 210, 15 ) );
                                            base.tx判定文字列[ 1 ].t2D描画( CDTXMania.app.Device, x_棒, y_棒, new Rectangle( 0, 110 + n棒高さ * 0, 210, n棒高さ ) );

                                            base.tx判定文字列[ 0 ].vc拡大縮小倍率 = new Vector3(base.st状態[ j ].fX方向拡大率, base.st状態[ j ].fY方向拡大率, 1f );
                                            base.tx判定文字列[ 0 ].fZ軸中心回転 = base.st状態[ j ].fZ軸回転度;
                                            base.tx判定文字列[ 0 ].n透明度 = 255;
                                            base.tx判定文字列[ 0 ].t2D描画( CDTXMania.app.Device, x, y, new Rectangle( 0, 0, nRectX, nRectY ) );

                                            base.tx判定文字列[ 2 ].vc拡大縮小倍率 = new Vector3( base.st状態[ j ].fX方向拡大率B, base.st状態[ j ].fY方向拡大率B, 1f );
                                            base.tx判定文字列[ 2 ].fZ軸中心回転 = base.st状態[ j ].fZ軸回転度;
                                            base.tx判定文字列[ 2 ].n透明度 = base.st状態[ j ].n透明度B;
                                            base.tx判定文字列[ 2 ].b加算合成 = true;
                                            base.tx判定文字列[ 2 ].t2D描画( CDTXMania.app.Device, x, y, new Rectangle( 0, 0, nRectX, nRectY ) );
                                        }
                                        break;
                                    case E判定.Great:
                                        {
                                            base.tx判定文字列[ 1 ].vc拡大縮小倍率 = new Vector3( base.st状態[ j ].fX方向拡大率_棒, base.st状態[ j ].fY方向拡大率_棒, 1f );
                                            base.tx判定文字列[ 1 ].fZ軸中心回転 = base.st状態[ j ].fZ軸回転度_棒;
                                            //base.tx判定文字列[ 1 ].t2D描画( CDTXMania.app.Device, x_棒, y_棒, new Rectangle( 0, 125, 210, 15 ) );
                                            base.tx判定文字列[ 1 ].t2D描画( CDTXMania.app.Device, x_棒, y_棒, new Rectangle( 0, 110 + n棒高さ * 1, 210, n棒高さ ) );

                                            base.tx判定文字列[ 0 ].vc拡大縮小倍率 = new Vector3( base.st状態[ j ].fX方向拡大率, base.st状態[ j ].fY方向拡大率, 1f );
                                            base.tx判定文字列[ 0 ].fZ軸中心回転 = base.st状態[ j ].fZ軸回転度;
                                            base.tx判定文字列[ 0 ].n透明度 = 255;
                                            base.tx判定文字列[ 0 ].t2D描画( CDTXMania.app.Device, x, y, new Rectangle( nRectX + 5, 0, nRectX, nRectY ) );
                                        }
                                        break;
                                    case E判定.Good:
                                        {
                                            base.tx判定文字列[ 0 ].vc拡大縮小倍率 = new Vector3(base.st状態[j].fX方向拡大率, base.st状態[j].fY方向拡大率, 1f);
                                            base.tx判定文字列[ 0 ].fZ軸中心回転 = 0;
                                            base.tx判定文字列[ 0 ].n透明度 = 255;
                                            base.tx判定文字列[ 0 ].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, nRectY + 2, nRectX, nRectY));
                                        }
                                        break;
                                    case E判定.Poor:
                                        {
                                            base.tx判定文字列[ 0 ].vc拡大縮小倍率 = new Vector3(1f, 1f, 1f);
                                            base.tx判定文字列[ 0 ].fZ軸中心回転 = base.st状態[j].fZ軸回転度;
                                            base.tx判定文字列[ 0 ].n透明度 = base.st状態[j].n透明度;
                                            base.tx判定文字列[ 0 ].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX + 5, nRectY + 2, nRectX, nRectY));
                                        }
                                        break;
                                    case E判定.Miss:
                                    case E判定.Bad:
                                        {
                                            base.tx判定文字列[ 0 ].vc拡大縮小倍率 = new Vector3(1f, 1f, 1f);
                                            base.tx判定文字列[ 0 ].fZ軸中心回転 = base.st状態[j].fZ軸回転度;
                                            base.tx判定文字列[ 0 ].n透明度 = base.st状態[j].n透明度;
                                            base.tx判定文字列[ 0 ].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, nRectY * 2 + 4, nRectX, nRectY));
                                        }
                                        break;
                                    case E判定.Auto:
                                        {
                                            base.tx判定文字列[ 0 ].vc拡大縮小倍率 = new Vector3( base.st状態[ j ].fX方向拡大率, base.st状態[ j ].fY方向拡大率, 1f );
                                            base.tx判定文字列[ 0 ].fZ軸中心回転 = base.st状態[ j ].fZ軸回転度;
                                            base.tx判定文字列[ 0 ].n透明度 = 255;
                                            base.tx判定文字列[ 0 ].t2D描画( CDTXMania.app.Device, x + 5, y, new Rectangle(nRectX * 2 + 3, nRectY * 2 + 4, nRectX, nRectY ) );
                                        }
                                        break;
                                }

                                if (base.nShowLagType == (int)EShowLagType.ON ||
                                     ((base.nShowLagType == (int)EShowLagType.GREAT_POOR) && (base.st状態[j].judge != E判定.Perfect)))
                                {
                                    if (base.st状態[j].judge != E判定.Auto && base.txlag数値 != null)		// #25370 2011.2.1 yyagi
                                    {
                                        bool minus = false;
                                        int offsetX = 0;
                                        string strDispLag = base.st状態[j].nLag.ToString();
                                        if (st状態[j].nLag < 0)
                                        {
                                            minus = true;
                                        }
                                        //x = xc - strDispLag.Length * 15 / 2;
                                        x = ( ( num5 ) + (this.stレーンサイズ[j].w / 2) ) - strDispLag.Length * 15 / 2;
                                        for (int i = 0; i < strDispLag.Length; i++)
                                        {
                                            int p = (strDispLag[i] == '-') ? 11 : (int)(strDispLag[i] - '0');	//int.Parse(strDispLag[i]);
                                            p += minus ? 0 : 12;		// change color if it is minus value
                                            base.txlag数値.t2D描画(CDTXMania.app.Device, x + offsetX, y + 34, base.stLag数値[p].rc);
                                            offsetX += 15;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
			}
			return 0;
		}
		

		// その他

		#region [ private ]
		//-----------------
        //HH SD BD HT LT FT CY HHO RD LC LP LBD
        //private readonly int[] n文字の縦表示位置 = new int[] { -1, 1, 1, 2, 0, 0, 1, -1, 2, 1, 2, -1, -1, 0, 0 };
        private readonly int[] n文字の縦表示位置 = new int[] { 0, 9, 9, 14, 4, 4, 9, 0, 9, 4, 0, 0, 0, 0, 0 };
		private STレーンサイズ[] stレーンサイズ;
		//-----------------
		#endregion
	}
}
