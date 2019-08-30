using UnityEngine;
using LibPDBinding;
using System.Threading;
using unitythread;
using System.Collections;

public class bouncingBall : MonoBehaviour
{
    private float y;
    public Rigidbody ball;
    private Renderer rend;
    private Material material;
    public float instrumento;
    public float velocidade;
    public float alturaMaxima;
    public float alturaMinima;
    public Vector3 velocity ;
    public float tempo;
    public Vector3 aceleracao;
    private Vector3 lastVelocity;
    public float y_atual;
    private bool inicio = true;
    
    // [HideInInspector]//Esconde variavel na interface

    private float ySpeed = 5f;
    public float height = 2f;
    bool goingUp = true;
    float startY;
    [SerializeField] //mostra variavel na interface
    Rigidbody _rig;

    void Awake() {
      this._rig = GetComponent<Rigidbody>();
      material = GetComponent<Renderer>().material;
      startY = this._rig.position.y;
      material.color = new Color(UnityEngine.Random.Range(1, 255) / 255f, UnityEngine.Random.Range(1, 255) / 255f, UnityEngine.Random.Range(1, 255) / 255f);
    }

    void FixedUpdate()
    {
      if (this.goingUp){
          if (this._rig.position.y >= this.startY + this.height) {
              this.goingUp = false;
              float extra_delta = Mathf.Abs(this._rig.position.y - (this.startY + this.height));
              this._rig.position = new Vector3(this._rig.position.x, this.startY + this.height - extra_delta, this._rig.position.z);
          }
      } else {
          if (this._rig.position.y <= this.startY) {
              PlaySound();
              this.goingUp = true;
              float extra_delta = Mathf.Abs(this._rig.position.y - this.startY);
              this._rig.position = new Vector3(this._rig.position.x, this.startY + extra_delta, this._rig.position.z);
          }
      }
      this._rig.velocity = Vector3.up * ySpeed * (goingUp ? 1 : -1);
    }

    // void Start()
    // {
    //     inicio = true;
    //     //Debug.Log(velocidade);
    //     ball = GetComponent<Rigidbody>();
    //     material = GetComponent<Renderer>().material;
    //     material.color = new Color(UnityEngine.Random.Range(1, 255) / 255f, UnityEngine.Random.Range(1, 255) / 255f, UnityEngine.Random.Range(1, 255) / 255f);
    //     ForceToDown(300);

    // }

    // void FixedUpdate() {
    //     // Debug.Log(ball.velocity);
    //     if ((ball.position.y >= alturaMaxima) && (!inicio)) {
    //         if (alturaMaxima == 4)
    //           Debug.Log(ball.position.y);
    //         ForceToDown(300);
    //     }
    //     if (ball.position.y <= alturaMinima) {
    //         if (inicio) 
    //             inicio = false;
    //         ForceToUp(300);
    //     }        
    // }
    
    // void ForceToUp(float velocity) {
    //     ClearForce();
    //     ball.AddForce(Vector3.up * velocity, ForceMode.Acceleration);
    // }

    // void ForceToDown(float velocity) {
    //     ClearForce();
    //     ball.AddForce(Vector3.down * velocity, ForceMode.Acceleration);
    // }
    // void ClearForce() {
    //   ball.velocity = new Vector3(0,0,0);
    //   material.color = new Color(UnityEngine.Random.Range(1, 255) / 255f, UnityEngine.Random.Range(1, 255) / 255f, UnityEngine.Random.Range(1, 255) / 255f);

    // }

    void PlaySound() {
        LibPD.SendFloat("som", instrumento);
    }
}
