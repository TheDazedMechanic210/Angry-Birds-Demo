using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehav : MonoBehaviour
{
    Vector3 mousePos;
    Vector3 mousePosBeta;
    Vector2 actualMousePos;
    Vector2 springPos;
    Vector3 startPos;
    private Rigidbody2D rb;
    private SpringJoint2D spring;
    private LineRenderer band;
    private GameObject anchorSpring;

    private Ray mouseRay;

    public float stretchLimit;
    private float stretchLimitSqr;
    private Vector3 springPosLocal;
    private bool isPressed;
    private bool isClose;
    public float shootDist;
    public LineRenderer stringFront;
    public LineRenderer stringBack;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anchorSpring = GameObject.FindGameObjectWithTag("PrimarySlingshot");
        spring = GetComponent<SpringJoint2D>();
        springPos = anchorSpring.transform.position;
        rb.gravityScale = 0.0f;
        isClose = false;
        startPos = transform.position;
    }


    void Update()
    {

        if (isPressed)
        {
            StringUpdate();
            actualMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(actualMousePos, springPos) > stretchLimit)
            {
                mousePosBeta = springPos + (actualMousePos - springPos).normalized * stretchLimit;
                rb.position = mousePosBeta;
            }
            else
            {
                rb.position = actualMousePos;
            }

        }

        if (isClose)
        {
            if (Vector3.Distance(springPos, transform.position) < shootDist)
            {
                spring.enabled = false;
                rb.gravityScale = 1;
            }
        }

        if (rb.velocity.magnitude == 0f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                transform.position = startPos;
                rb.gravityScale = 0.0f;
                isClose = false;
                spring.enabled=true;
            }
        }

    }

    void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
        isClose = false;
        StringUpdate();
    }

    void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;
        isClose = true;
        stringBack.enabled = false;
        stringFront.enabled = false;
        stringBack.positionCount = 0;
        stringFront.positionCount = 0;

    }

    void StringUpdate()
    {
        stringBack.positionCount = 2;
        stringFront.positionCount = 2;
        stringBack.enabled = true;
        stringFront.enabled = true;
        stringBack.SetPosition(0, stringBack.gameObject.transform.position);
        stringFront.SetPosition(0, stringFront.gameObject.transform.position);
        stringBack.SetPosition(1, transform.position);
        Vector3 actualPosition = transform.position;
        actualPosition.x -= 0.25f;
        stringFront.SetPosition(1, actualPosition);
    }


}
