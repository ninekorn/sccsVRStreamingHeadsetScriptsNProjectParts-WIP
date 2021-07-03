using System;
using System.Collections.Generic;
using System.Text;

using SharpDX;
using sccoresystems.sccsgraphics;
using Jitter;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.LinearMath;
using Jitter.Collision.Shapes;
using Jitter.Forces;

using SC_message_object = sccoresystems.sccsmessageobject;
using SC_message_object_jitter = sccoresystems.sc_message_object_jitter;

using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Ab3d.OculusWrap;
//using SC_WPF_RENDER;
//using SC_WPF_RENDER.SC_Graphics;
//using SC_WPF_RENDER.SC_Graphics.SC_Grid;

using Ab3d.OculusWrap.DemoDX11;

using System.Runtime.InteropServices;

using System.IO;

using Jitter.DataStructures;
using SingleBodyConstraints = Jitter.Dynamics.Constraints.SingleBody;
//using Jitter.Dynamics.Constraints.SingleBody;
//using Jitter.LinearMath;
using Jitter.Dynamics.Constraints;
//using Jitter.Dynamics.Joints;
//using Jitter.Forces;

//using DeltaEngine.Datatypes;
//using DeltaEngine.Core;
//using DeltaEngine.Extensions;

using Vector2 = SharpDX.Vector2;
using Vector3 = SharpDX.Vector3;
using Vector4 = SharpDX.Vector4;
using Quaternion = SharpDX.Quaternion;
using Matrix = SharpDX.Matrix;
using Plane = SharpDX.Plane;
using Ray = SharpDX.Ray;

//using SCCoreSystems.SC_Graphics.SC_Grid;

using SharpDX.Multimedia;
using SharpDX.IO;
using System.Xml;
using SharpDX.XAudio2;
using System.Linq;

//using System.Windows.Forms;

using SharpDX.Direct3D;
using SharpDX.Direct3D11;

using System.Drawing;

using Rectangle = SharpDX.Rectangle;

using SharpDX.WIC;
using System.Drawing.Imaging;
using SharpDX.D3DCompiler;
using SharpDX.DXGI;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Interop;
using Device = SharpDX.Direct3D11.Device;
using Resource = SharpDX.Direct3D11.Resource;
using SharpDX.DirectInput;
using System.Reflection;
using System.Runtime;
using System.Runtime.CompilerServices;

//using SCCoreSystems.SC_Graphics;


using System.IO.Ports;

namespace sccoresystems.sccsgraphics
{
    public class sccsgraphicsupdatessec //: SC_Update//SC_Intermediate_Update
    {
        public sccsgraphicsupdatessec() //SC_console_directx _SC_console_directx, IntPtr _HWND
        {
          
        }

        public SC_message_object_jitter[][] _sc_create_world_objects(SC_message_object_jitter[][] _sc_jitter_tasks)
        {

            return _sc_jitter_tasks;
        }

        public unsafe SC_message_object_jitter[][] workOnSomething(SC_message_object_jitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft, Matrix oriProjectionMatrix)
        {
            
            return _sc_jitter_tasks;
        }

        public SC_message_object_jitter[][] sc_write_to_buffer(SC_message_object_jitter[][] _sc_jitter_tasks)
        {
            return _sc_jitter_tasks;
        }

        public SC_message_object_jitter[][] loop_worlds(SC_message_object_jitter[][] _sc_jitter_tasks, Matrix originRoter, Matrix rotatingMatrixer, Matrix hmdmatrixRoter, Matrix hmd_matrixer, Matrix rotatingMatrixForPelviser, Matrix _rightTouchMatrixer, Matrix _leftTouchMatrixer)
        {

            return _sc_jitter_tasks;
        }
    }
}



