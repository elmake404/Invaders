using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side
{
    Width, Length
}

public class Field : MonoBehaviour
{
    [SerializeField]
    private Color _color = Color.blue;
    [SerializeField]
    private Vector3 _offSet;
    private Vector3 _center
    { get { return transform.position + _offSet; } }
    [SerializeField]
    private float _width, _length;
    private float _widthFarPoint
    { get { return _center.x + _width; } }
    private float _widthNearPoint
    { get { return _center.x - _width; } }
    private float _lengthFarPoint
    { get { return _center.z + _length; } }
    private float _lengthNearPoint
    { get { return _center.z - _length; } }

    //[SerializeField]
    //private bool _enableDisplayOfTheField;

    private bool PositionCheckAbscissa(Vector3 position)
    {
        if (_widthFarPoint < position.x)
        {
            return false;
        }
        else if (_widthNearPoint > position.x)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private bool PositionCheckApplicate(Vector3 position)
    {
        if (_lengthFarPoint < position.z)
        {
            return false;
        }
        else if (_lengthNearPoint > position.z)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //возвращает крайнюю точку по оси апликат и рандомную по оси абсцис
    public Vector3 GetPoint()
    {
        Vector3 postiom = new Vector3(Random.Range(_widthNearPoint, _widthFarPoint), 0, _lengthFarPoint);
        return postiom;
    }

    //возвращает рандомные точки по оси апликат и по оси абсцис.  
    //если выставить флаг isOnlyForward=true точка по оси апликат будет меньше чем position
    public Vector3 GetPoint(Vector3 position, bool isOnlyForward)
    {
        float zMin;
        if (isOnlyForward)
        {
            Debug.Log(position.z < _lengthFarPoint);
            zMin = position.z < _lengthFarPoint ? position.z : _lengthFarPoint;
        }
        else
        {
            zMin = _lengthFarPoint;
        }

        Vector3 postiom = new Vector3(Random.Range(_widthNearPoint, _widthFarPoint), 0, Random.Range(_lengthNearPoint, zMin));
        return postiom;
    }

    //возвращает рандомные точки по оси апликат и по оси абсцис, c выставлением максимального шага на одну из сторон.
    // если выставить флаг isOnlyForward=true точка по оси апликат будет меньше чем position
    public Vector3 GetPoint(Vector3 position, float maxDistance, Side side, bool isOnlyForward)
    {
        float Z;
        if (isOnlyForward)
        {
            Z = position.z < _lengthFarPoint ? position.z : _lengthFarPoint;
        }
        else
        {
            Z = _lengthFarPoint;
        }

        float XMax;
        float XMin;

        float ZMax;
        float ZMin;

        if (side == Side.Length)
        {
            XMax = _widthFarPoint;
            XMin = _widthNearPoint;
            if (PositionCheckApplicate(position))
            {
                ZMax = position.z + maxDistance > Z ? Z : position.z + maxDistance;
                ZMin = position.z - maxDistance < _lengthNearPoint ? _lengthNearPoint : position.z - maxDistance;
            }
            else
            {
                ZMax = _lengthFarPoint;
                ZMin = _center.z;
            }
        }
        else
        {
            XMax = position.x + maxDistance > _widthFarPoint ? _widthFarPoint : position.x + maxDistance;
            XMin = position.x - maxDistance < _widthNearPoint ? _widthNearPoint : position.x - maxDistance;

            if (PositionCheckApplicate(position))
            {
                ZMax = Z;
                ZMin = _lengthNearPoint;
            }
            else
            {
                ZMax = _lengthFarPoint;
                ZMin = _center.z;
            }

        }



        Vector3 postiom = new Vector3(Random.Range(XMin, XMax), 0, Random.Range(ZMin, ZMax));
        return postiom;
    }
    //возвращает рандомные точки по оси апликат и по оси абсцис, c выставлением максимального шага на обе стороны.
    // если выставить флаг isOnlyForward=true точка по оси апликат будет меньше чем position
    public Vector3 GetPoint(Vector3 position, float maxDistanceX, float maxDistanceZ, bool isOnlyForward)
    {
        float Z;

        if (isOnlyForward)
        {
            Z = position.z < _lengthFarPoint ? position.z : _lengthFarPoint;
        }
        else
        {
            Z = _lengthFarPoint;
        }

        float XMax = position.x + maxDistanceX > _widthFarPoint ? _widthFarPoint : position.x + maxDistanceX;
        float XMin = position.x - maxDistanceX < _widthNearPoint ? _widthNearPoint : position.x - maxDistanceX;

        float ZMax = position.z + maxDistanceZ > Z ? Z : position.z + maxDistanceZ;
        float ZMin = position.z - maxDistanceZ < _lengthNearPoint ? _lengthNearPoint : position.z - maxDistanceZ;

        Vector3 postiom = new Vector3(Random.Range(XMin, XMax), 0, Random.Range(ZMin, ZMax));
        return postiom;
    }

    //возвращает рандомные точки по оси апликат и по оси абсцис, c выставлением максимального шага по оси апликат.
    //точка по оси апликат будет больше чем position
    // если выставить флаг isExactDistance=true точка по оси апликат будет удалена на максимальное растояние
    public Vector3 GetPointBeck(Vector3 position, float maxBeckDistance, bool isExactDistance)
    {
        float Z;

        Z = position.z > _lengthNearPoint ? position.z : _lengthNearPoint;

        float XMax;
        float XMin;

        float ZMax;
        float ZMin;

        XMax = _widthFarPoint;
        XMin = _widthNearPoint;

        ZMax = position.z + maxBeckDistance > _lengthFarPoint ? _lengthFarPoint : position.z + maxBeckDistance;
        ZMin = position.z - maxBeckDistance < Z ? Z : position.z - maxBeckDistance;
        Vector3 postion;
        if (!isExactDistance)
            postion = new Vector3(Random.Range(XMin, XMax), 0, Random.Range(ZMin, ZMax));
        else
            postion = new Vector3(Random.Range(XMin, XMax), 0, ZMax);

        return postion;
    }

    //возвращает рандомные точки по оси апликат и по оси абсцис.
    public Vector3 GetPointBeck(Vector3 position)
    {
        float Z;

        Z = position.z > _lengthNearPoint ? position.z : _lengthNearPoint;

        float XMax;
        float XMin;

        float ZMax;
        float ZMin;

        XMax = _widthFarPoint;
        XMin = _widthNearPoint;

        ZMax = _lengthFarPoint;
        ZMin = Z;

        Vector3 postiom = new Vector3(Random.Range(XMin, XMax), 0, Random.Range(ZMin, ZMax));
        return postiom;
    }

    private void OnDrawGizmos/*Selected*/()
    {
            Gizmos.color = _color;

            Gizmos.DrawCube(_center, new Vector3(_width * 2, 0.0001f, _length * 2));
    }
    }
