using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaderManager : MonoBehaviour
{
	public Texture  hdri;
	public Texture noise;
	void Start () {
		Shader.SetGlobalTexture("_HdriGlobal",hdri);
		Shader.SetGlobalTexture("_NoiseGlobal",noise);
	}

}
