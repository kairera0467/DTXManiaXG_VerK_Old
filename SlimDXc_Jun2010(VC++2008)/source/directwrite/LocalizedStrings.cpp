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

#include "../stack_array.h"

#include "DirectWriteException.h"

#include "LocalizedStrings.h"

const IID IID_IDWriteLocalizedStrings = __uuidof(IDWriteLocalizedStrings);

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	int LocalizedStrings::Count::get()
	{
		return InternalPointer->GetCount();
	}

	System::String^ LocalizedStrings::LocaleName::get(int index)
	{
		if(index >= Count)
			throw gcnew ArgumentOutOfRangeException("index");

		UINT32 length = 0;
		HRESULT hr = InternalPointer->GetLocaleNameLength(index, &length);
		if(RECORD_DW(hr).IsFailure)
			return nullptr;

		stack_array<wchar_t> buffer = stackalloc(wchar_t, length);
		hr = InternalPointer->GetLocaleName(index, &buffer[0], length);
		if(RECORD_DW(hr).IsFailure)
			return nullptr;

		return gcnew System::String(&buffer[0]);
	}

	System::String^ LocalizedStrings::String::get(int index)
	{
		if(index >= Count)
			throw gcnew ArgumentOutOfRangeException("index");

		UINT32 length = 0;
		HRESULT hr = InternalPointer->GetStringLength(index, &length);
		if(RECORD_DW(hr).IsFailure)
			return nullptr;

		stack_array<wchar_t> buffer = stackalloc(wchar_t, length);
		hr = InternalPointer->GetString(index, &buffer[0], length);
		if(RECORD_DW(hr).IsFailure)
			return nullptr;

		return gcnew System::String(&buffer[0]);
	}

	int LocalizedStrings::FindLocaleName( System::String^ localeName, [Out] bool% exists )
	{
		BOOL existsNative = FALSE;
		UINT32 index = 0;
		pin_ptr<const wchar_t> nameChars = PtrToStringChars( localeName );
		HRESULT hr = InternalPointer->FindLocaleName( nameChars, &index, &existsNative );
		exists = existsNative == TRUE;
		RECORD_DW(hr);
		return index;
	}
}
}