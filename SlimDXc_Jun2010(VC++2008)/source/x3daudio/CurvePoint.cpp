#include "stdafx.h"
/*
* Copyright (c) 2007-2010 SlimDX Group
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

#include "CurvePoint.h"

using namespace System;

namespace SlimDX
{
namespace X3DAudio
{
	bool CurvePoint::operator == ( CurvePoint left, CurvePoint right )
	{
		return CurvePoint::Equals( left, right );
	}

	bool CurvePoint::operator != ( CurvePoint left, CurvePoint right )
	{
		return !CurvePoint::Equals( left, right );
	}

	int CurvePoint::GetHashCode()
	{
		return Distance.GetHashCode() + DspSetting.GetHashCode();
	}

	bool CurvePoint::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<CurvePoint>( value ) );
	}

	bool CurvePoint::Equals( CurvePoint value )
	{
		return ( Distance == value.Distance && DspSetting == value.DspSetting );
	}

	bool CurvePoint::Equals( CurvePoint% value1, CurvePoint% value2 )
	{
		return ( value1.Distance == value2.Distance && value1.DspSetting == value2.DspSetting );
	}
}
}