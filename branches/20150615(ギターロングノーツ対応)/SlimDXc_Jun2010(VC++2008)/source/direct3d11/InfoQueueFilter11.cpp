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
#include "stdafx.h"

#include "Direct3D11Exception.h"
#include "InfoQueueFilter11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{
	void InfoQueueFilter::Count(int &categories, int &severities, int &ids)
	{
		if (AllowList.CategoryList != nullptr)
			categories += AllowList.CategoryList->Length;
		if (AllowList.SeverityList != nullptr)
			severities += AllowList.SeverityList->Length;
		if (AllowList.MessageIds != nullptr)
			ids += AllowList.MessageIds->Length;

		if (DenyList.CategoryList != nullptr)
			categories += DenyList.CategoryList->Length;
		if (DenyList.SeverityList != nullptr)
			severities += DenyList.SeverityList->Length;
		if (DenyList.MessageIds != nullptr)
			ids += DenyList.MessageIds->Length;
	}
}
}