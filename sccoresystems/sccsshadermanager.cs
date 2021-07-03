using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Runtime.InteropServices;

namespace sccoresystems.sccsgraphics
{
    public class sccsshadermanager                 // 77 lines
    {
        

        Vector4 ambientColor = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
        Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
        Vector3 lightDirection = new Vector3(1, 0, 0);
        Vector3 lightPosition = new Vector3(0, 0, 0);


        /*
        BufferDescription lightBufferDesc = new BufferDescription()
        {
            Usage = ResourceUsage.Dynamic,
            SizeInBytes = Utilities.SizeOf<sc_voxel.DLightBuffer>(),
            BindFlags = BindFlags.ConstantBuffer,
            CpuAccessFlags = CpuAccessFlags.Write,
            OptionFlags = ResourceOptionFlags.None,
            StructureByteStride = 0
        };*/

        IntPtr _windowsHandle;
        Device _device;

        public bool Initialize(Device device, IntPtr windowsHandle) //, float x, float y, float z, Vector4 color,Matrix worldMatrix
        {

            _windowsHandle = windowsHandle;
            _device = device;
            
            return true;
        }
    }
}