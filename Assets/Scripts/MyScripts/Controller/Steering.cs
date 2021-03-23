using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SteeringSights
{
    right,
    left,
}

public class Steering : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float steer = 5;

    public SteeringSights steeringSights;

    bool upCheck = false;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        switch (steeringSights)
        {
            case SteeringSights.right :
                {
                    BaseController.instance.steering = (steer * (BaseController.instance.rb.velocity.y + BaseController.instance.maxSteeringAngle) * Time.deltaTime) / Mathf.PI * 2;
                    print(BaseController.instance.steering);
                    upCheck = false;
                }
                break;
            case SteeringSights.left:
                {
                    BaseController.instance.steering = (steer * (BaseController.instance.rb.velocity.y + BaseController.instance.maxSteeringAngle) * Time.deltaTime) / Mathf.PI * 2;
                    upCheck = false;
                    //ChangeLine(BaseController.instance.direction = -12f);
                }
                break;
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        switch (steeringSights)
        {
            case SteeringSights.right:
                {
                    upCheck = true;
                    Corrector();
                    //BaseController.instance.steering = 0;
                    //GameManager.instance.mainPlayerInstance.transform.rotation = Quaternion.Euler(GameManager.instance.mainPlayerInstance.transform.rotation.x,0f, GameManager.instance.mainPlayerInstance.transform.rotation.z);
                }
                break;
            case SteeringSights.left:
                {
                    upCheck = true;
                    Corrector();
                    //GameManager.instance.mainPlayerInstance.transform.rotation = Quaternion.Euler(GameManager.instance.mainPlayerInstance.transform.rotation.x, 0f, GameManager.instance.mainPlayerInstance.transform.rotation.z);
                    //ChangeLine(BaseController.instance.direction = 0);
                }
                break;
        }
    }
    void Corrector()
    {
        if (upCheck==true)
        {
            /*while (BaseController.instance.rb.rotation.y != 0)
            {
                //do quaternion lerp effects only to rb.rotation.y not whole rotation 
                //make smoother lerp and more  beatiful 
                BaseController.instance.rb.rotation = Quaternion.Lerp(BaseController.instance.rb.rotation, Quaternion.identity, 25 * Time.fixedDeltaTime);
                BaseController.instance.rb.velocity = Vector3.zero;
                BaseController.instance.steering = 0;
                if (BaseController.instance.rb.rotation.y == 0)
                    break;
            }*/
            
            //while (BaseController.instance.steering > 0)
            //{
            //    print(BaseController.instance.steering);
            //    BaseController.instance.steering = (steer * (BaseController.instance.rb.velocity.y - BaseController.instance.maxSteeringAngle) * Time.deltaTime) / Mathf.PI * 2;
            //    print("Steering: " +BaseController.instance.steering);
            //    print("Vel: " + BaseController.instance.rb.velocity.y);
            //    if (BaseController.instance.steering <= 0)
            //    {
            //        BaseController.instance.rb.velocity = Vector3.zero;
            //        BaseController.instance.steering = 0;
            //        BaseController.instance.rb.rotation = Quaternion.Lerp(BaseController.instance.rb.rotation, Quaternion.identity, Time.deltaTime);
            //
            //        break;
            //    }
            //}
            //do
            //{
            //    BaseController.instance.steering = steer + 0.5f * Time.deltaTime;
            //    print(BaseController.instance.steering);
            //    if (BaseController.instance.steering == 0)
            //        break;
            //    //GameManager.instance.mainPlayerInstance.transform.rotation = Quaternion.Euler(GameManager.instance.mainPlayerInstance.transform.rotation.x, 0f, GameManager.instance.mainPlayerInstance.transform.rotation.z);
            //} while (BaseController.instance.steering == 0);
            print("done");
        }
        upCheck = false;
        //BaseController.instance.rb.velocity = Vector3.zero;
    }
}

    /*IEnumerator CarTurn(float id)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            obj.transform.localPosition = new Vector3(id + obj.transform.localPosition.x, obj.transform.localPosition.y,obj.transform.localPosition.z);

            if (obj.transform.localPosition.y > 1)
                obj.transform.localPosition = new Vector3(obj.transform.position.x, 0.68f, transform.localPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(duration);
    }*/
    

