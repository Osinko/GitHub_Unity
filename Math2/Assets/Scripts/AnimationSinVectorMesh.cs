using UnityEngine;
using System.Collections;

public class AnimationSinVectorMesh : VectorMesh {

	public float Amplitude;	//振幅
	public float speed;		//周期速度
	public float theta;		//現在のθ

	float diffTheta;
	Vector3 sinMovePos;

	public override void Awake ()
	{
		Amplitude=3;
		speed =5;		//５にすると５秒で１回転
		theta =0;

		ParticleSystem ps = gameObject.AddComponent<ParticleSystem>();


		base.Awake ();
	}


	public override void Update ()
	{
		diffTheta = Mathf.PI * 2 /speed;
		theta += diffTheta * Time.deltaTime;
		theta %= Mathf.PI * 2;

		sinMovePos = new Vector3( 0 , Mathf.Sin(theta) , 0 );

		base.Update ();
	}
}
