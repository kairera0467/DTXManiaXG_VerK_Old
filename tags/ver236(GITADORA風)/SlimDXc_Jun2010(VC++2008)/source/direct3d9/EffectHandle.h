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
#pragma once

namespace SlimDX
{
	namespace Direct3D9
	{
		/// <summary>A handle to an effect or an object inside an effect.</summary>
		/// <unmanaged>D3DXHANDLE</unmanaged>
		public ref class EffectHandle : System::IEquatable<EffectHandle^>
		{
		private:
			D3DXHANDLE m_Handle;
			System::IntPtr m_StringData;
			System::Int64 m_StringDataSize;
			bool m_HasString;

		internal:
			EffectHandle( D3DXHANDLE handle );

			property D3DXHANDLE InternalHandle
			{
				D3DXHANDLE get() { return m_Handle; }
			}

			void Destruct();

		public:
			[System::Security::Permissions::SecurityPermission( System::Security::Permissions::SecurityAction::LinkDemand, Flags=System::Security::Permissions::SecurityPermissionFlag::UnmanagedCode )]
			EffectHandle( System::String^ name );
			virtual	~EffectHandle() { Destruct(); }
			!EffectHandle() { Destruct(); }

			static operator EffectHandle^ ( System::String^ name );

			static bool operator == ( EffectHandle^ left, EffectHandle^ right );

			static bool operator != ( EffectHandle^ left, EffectHandle^ right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( EffectHandle^ other );
			static bool Equals( EffectHandle^ value1, EffectHandle^ value2 );
		};
	}
}
