using System;
using System.Collections.Generic;

namespace VoxelWorld.Classes.Engine
{
    // Главный класс от которого наследуються другие, для размещения их в List с детьми узлов
    public class Node
    {
        public virtual void Ready() {

        }
        
        public virtual void RenderProcess(){
        }
        
        public virtual void PhysicsProcess(){
        }

        public virtual void OnResizeWindow(EventArgs e) {
        }
    }
}