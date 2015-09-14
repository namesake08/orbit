using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class LineCreator
    {
        public static Material CreateLineMaterial()
        {
            Material lineMaterial;
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            var shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
            
            return lineMaterial;
        }

        public static void DrawLine(Vector3 pos1, Vector3 pos2)
        {
            GL.PushMatrix();

            //// Set transformation matrix for drawing to
            //// match our transform
            //GL.MultMatrix(transform.localToWorldMatrix);

            // Draw line
            GL.Begin(GL.LINES);
            GL.Color(new Color(1, 0, 0));
            GL.Vertex3(pos1.x, pos1.y, pos1.z);
            GL.Vertex3(pos2.x, pos2.y, pos2.z);
            GL.End();
            GL.PopMatrix();
        }
    }
}
