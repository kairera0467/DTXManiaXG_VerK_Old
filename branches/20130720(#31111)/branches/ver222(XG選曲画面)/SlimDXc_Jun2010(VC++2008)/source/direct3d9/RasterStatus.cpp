#include "stdafx.h"
/*
* Copyright (c) 2007-2012 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/
#include <d3d9.h>
#include <d3dx9.h>

#include "RasterStatus.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool RasterStatus::operator == ( RasterStatus left, RasterStatus right )
	{
		return RasterStatus::Equals( left, right );
	}

	bool RasterStatus::operator != ( RasterStatus left, RasterStatus right )
	{
		return !RasterStatus::Equals( left, right );
	}

	int RasterStatus::GetHashCode()
	{
		return InVBlank.GetHashCode() + Scanline.GetHashCode();
	}

	bool RasterStatus::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<RasterStatus>( value ) );
	}

	bool RasterStatus::Equals( RasterStatus value )
	{
		return ( InVBlank == value.InVBlank && Scanline == value.Scanline );
	}

	bool RasterStatus::Equals( RasterStatus% value1, RasterStatus% value2 )
	{
		return ( value1.InVBlank == value2.InVBlank && value1.Scanline == value2.Scanline );
	}
}
}