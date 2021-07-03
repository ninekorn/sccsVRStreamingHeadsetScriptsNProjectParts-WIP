using System;
using System.Collections.Generic;
using System.Text;
using SharpDX;
using SC_message_object = sccoresystems.sccsmessageobject;
using SC_message_object_jitter = sccoresystems.sc_message_object_jitter;
using Ab3d.OculusWrap;

namespace sccoresystems.sccsgraphics
{
    public abstract class scintermediateupdate : sccssharpdxdirectx
    {
        protected override void scinitdirectx() //DsSystemConfiguration configuration, IntPtr Hwnd, sc_console.sc_console_writer _writer
        {
            base.scinitdirectx(); //configuration, Hwnd, _writer
        }

        public abstract SC_message_object_jitter[][] sc_write_to_buffer(SC_message_object_jitter[][] _sc_jitter_tasks);
        public abstract SC_message_object_jitter[][] loop_worlds(SC_message_object_jitter[][] _sc_jitter_tasks);
        public abstract SC_message_object_jitter[][] workOnSomething(SC_message_object_jitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft);
        public abstract SC_message_object_jitter[][] _sc_create_world_objects(SC_message_object_jitter[][] _sc_jitter_tasks);

    }
}
