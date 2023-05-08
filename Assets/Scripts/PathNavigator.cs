using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNavigator : MonoBehaviour
{
    public enum PathMode
    {
        Loop,
        Return,
        Random
    }

    public enum TurnMode
    {
        None,
        TurnInPlace,
        Smooth
    }

    public float speed;
    public float angularSpeed;
    public PathMode pathMode;
    public TurnMode turnMode;
    public GameObject[] path;

    int _nodeIndex = -1;
    bool _returning;

    private void Awake()
    {
        _returning = false;
        if (angularSpeed == 0f)
        {
            // 0 would cause problems in a few different modes
            angularSpeed = 0.1f;
        }
        NextNode();
    }

    void NextNode()
    {
        if (path.Length == 0) 
        {
            Debug.LogWarning("Empty path");
            Destroy(this);
            return;
        }

        switch (pathMode)
        {
            case PathMode.Loop:
                {
                    _nodeIndex++;
                    if (_nodeIndex >= path.Length)
                    {
                        _nodeIndex = 0;
                    }
                }
                break;
            case PathMode.Return:
                {
                    if (_returning)
                    {
                        _nodeIndex--;
                        if ( _nodeIndex < 0 )
                        {
                            _returning = false;
                        }
                    }
                    if (!_returning)
                    {
                        _nodeIndex++;
                        if (_nodeIndex >= path.Length)
                        {
                            _nodeIndex--;
                            _returning = true;
                        }
                    }
                }
                break;
            case PathMode.Random:
                {
                    _nodeIndex = Random.Range(0, path.Length);
                }
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (path.Length == 0)
        {
            return;
        }

        Vector3 nodePos = path[_nodeIndex].transform.position;
        Vector3 delta = nodePos - transform.position;

        if (turnMode == TurnMode.None)
        {
            if (delta.sqrMagnitude < speed * Time.deltaTime)
            {
                NextNode();
            }
            else
            {
                transform.position += speed * Time.deltaTime * delta.normalized;
            }
        }

        if (turnMode != TurnMode.None)
        {
            if (delta.sqrMagnitude < speed * Time.deltaTime)
            {
                transform.position = nodePos;
            }

            Quaternion lookAtRotation = Quaternion.LookRotation(delta);
            float angle = Quaternion.Angle(lookAtRotation, transform.rotation);

            if (turnMode == TurnMode.TurnInPlace)
            {
                if (angle > Time.deltaTime * angularSpeed)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtRotation, Time.deltaTime * angularSpeed);
                }
                else
                {
                    if (delta.sqrMagnitude < speed * Time.deltaTime)
                    {
                        NextNode();
                    }
                    else
                    {
                        transform.SetPositionAndRotation(transform.position + speed * Time.deltaTime * delta.normalized, lookAtRotation);
                    }
                    
                }
            }   
            else
            {
                if (delta.sqrMagnitude < speed * Time.deltaTime)
                {
                    NextNode();
                }
                else
                {
                    transform.SetPositionAndRotation(transform.position + speed * Time.deltaTime * delta.normalized, Quaternion.RotateTowards(transform.rotation, lookAtRotation, Time.deltaTime * angularSpeed));
                }
            }
        }
    }
}
