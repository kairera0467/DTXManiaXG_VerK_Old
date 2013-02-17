﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CAct演奏判定文字列共通 : CActivity
	{
		// プロパティ
        public int iP_A;
        public int iP_B;
        public CCounter ctPERFECT;
        public CCounter ctGOOD;
        public CCounter ctPOOR;
		protected STSTATUS[] st状態 = new STSTATUS[ 15 ];
		[StructLayout( LayoutKind.Sequential )]
		protected struct STSTATUS
		{
			public CCounter ct進行;
			public E判定 judge;
			public float fX方向拡大率;
			public float fY方向拡大率;
			public int n相対X座標;
			public int n相対Y座標;
			public int n透明度;
			public int nLag;								// #25370 2011.2.1 yyagi
		}

		protected readonly ST判定文字列[] st判定文字列;
		[StructLayout( LayoutKind.Sequential )]
		protected struct ST判定文字列
		{
			public int n画像番号;
			public Rectangle rc;
		}

		protected readonly STlag数値[] stLag数値;			// #25370 2011.2.1 yyagi
		[StructLayout( LayoutKind.Sequential )]
		protected struct STlag数値
		{
			public Rectangle rc;
		}

		protected CTexture[] tx判定文字列 = new CTexture[ 3 ];
		protected CTexture txlag数値 = new CTexture();		// #25370 2011.2.1 yyagi

		public int nShowLagType							// #25370 2011.6.3 yyagi
		{
			get;
			set;
		}

		// コンストラクタ

		public CAct演奏判定文字列共通()
		{
            this.ctPERFECT = new CCounter(0, 11, 80, CDTXMania.Timer);
            this.ctGOOD = new CCounter(0, 4, 100, CDTXMania.Timer);
            this.ctPOOR = new CCounter(0, 7, 100, CDTXMania.Timer);
            
            this.ctPERFECT.t進行();
            this.ctGOOD.t進行();
            this.ctPOOR.t進行();
            int iP_A = 390;
            int iP_B = 0x248;
            int num1 = this.ctPERFECT.n現在の値;
            int num2 = this.ctGOOD.n現在の値;
			this.st判定文字列 = new ST判定文字列[ 7 ];
			Rectangle[] r = new Rectangle[] {
                //220x220がいいのかも。
#if 新処理
				new Rectangle( 0,   0 + (90 * num1), 150, 90 ),		// Perfect
				new Rectangle( 150, 0 + (90 * num1), 150, 90 ),		// Great
				new Rectangle( 300, 0 + (90 * num2), 150, 90 ),		// Good
				new Rectangle( 0, 0,    0x80, 0x2a ),		// Poor
				new Rectangle( 0, 0x2b, 0x80, 0x2a ),		// Miss
				new Rectangle( 0, 0x56, 0x80, 0x2a ),		// Bad
				new Rectangle( 0, 0,    0x80, 0x2a )		// Auto
#endif
                new Rectangle( 0, 0,    0x80, 0x2a ),		// Perfect
				new Rectangle( 0, 0x2b, 0x80, 0x2a ),		// Great
				new Rectangle( 0, 0x56, 0x80, 0x2a ),		// Good
				new Rectangle( 0, 0,    0x80, 0x2a ),		// Poor
				new Rectangle( 0, 0x2b, 0x80, 0x2a ),		// Miss
				new Rectangle( 0, 0x56, 0x80, 0x2a ),		// Bad
				new Rectangle( 0, 0,    0x80, 0x2a )		// Auto
			};

			for ( int i = 0; i < 7; i++ )
			{
				this.st判定文字列[ i ] = new ST判定文字列();
				this.st判定文字列[ i ].n画像番号 = i / 3;
				this.st判定文字列[ i ].rc = r[i];
			}

			this.stLag数値 = new STlag数値[ 12 * 2 ];		// #25370 2011.2.1 yyagi
			for ( int i = 0; i < 12; i++ )
			{
				this.stLag数値[ i      ].rc = new Rectangle( ( i % 4 ) * 15     , ( i / 4 ) * 19     , 15, 19 );	// plus numbers
				this.stLag数値[ i + 12 ].rc = new Rectangle( ( i % 4 ) * 15 + 64, ( i / 4 ) * 19 + 64, 15, 19 );	// minus numbers
			}
			base.b活性化してない = true;
		}


		// メソッド

		public virtual void Start( int nLane, E判定 judge, int lag )
		{
			if( ( nLane < 0 ) || ( nLane > 14 ) )
			{
				throw new IndexOutOfRangeException( "有効範囲は 0～14 です。" );
			}
			if( ( ( nLane >= 10 ) || ( ( (E判定文字表示位置) CDTXMania.ConfigIni.判定文字表示位置.Drums ) != E判定文字表示位置.表示OFF ) ) && ( ( ( nLane != 13 ) || ( ( (E判定文字表示位置) CDTXMania.ConfigIni.判定文字表示位置.Guitar ) != E判定文字表示位置.表示OFF ) ) && ( ( nLane != 14 ) || ( ( (E判定文字表示位置) CDTXMania.ConfigIni.判定文字表示位置.Bass ) != E判定文字表示位置.表示OFF ) ) ) )
			{

				this.st状態[ nLane ].ct進行 = new CCounter( 0, 300, 1, CDTXMania.Timer );
				this.st状態[ nLane ].judge = judge;
				this.st状態[ nLane ].fX方向拡大率 = 1f;
				this.st状態[ nLane ].fY方向拡大率 = 1f;
				this.st状態[ nLane ].n相対X座標 = 0;
				this.st状態[ nLane ].n相対Y座標 = 0;
				this.st状態[ nLane ].n透明度 = 0xff;
				this.st状態[ nLane ].nLag = lag;
			}
		}


		// CActivity 実装

		public override void On活性化()
		{
			for( int i = 0; i < 15; i++ )
			{
				this.st状態[ i ].ct進行 = new CCounter();
			}
			base.On活性化();
			this.nShowLagType = CDTXMania.ConfigIni.nShowLagType;
		}
		public override void On非活性化()
		{
			for( int i = 0; i < 15; i++ )
			{
				this.st状態[ i ].ct進行 = null;
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.tx判定文字列[0] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\ScreenPlay judge strings 1.png"));
                this.tx判定文字列[1] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\ScreenPlay judge strings 2.png"));
				this.tx判定文字列[ 2 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlay judge strings 3.png" ) );
                this.txlag数値 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_lag numbers.png"));
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.tx判定文字列[ 0 ] );
				CDTXMania.tテクスチャの解放( ref this.tx判定文字列[ 1 ] );
				CDTXMania.tテクスチャの解放( ref this.tx判定文字列[ 2 ] );
				CDTXMania.tテクスチャの解放( ref this.txlag数値 );
				base.OnManagedリソースの解放();
			}
		}
	}
}