using UnityEngine;

public class CircleModule : Module
{
    #region Fields
    [SerializeField] private Transform _circleOne;
    [SerializeField] private Transform _circleTwo;
    [SerializeField] private Transform _circleThree;

    [SerializeField, Range(1f, 100f)] private float _minSpeed = 1f;
    [SerializeField, Range(1f, 100f)] private float _maxSpeed = 50f;
    #endregion

    #region Variables
    private float _speed;
    private int _directionOne, _directionTwo, _directionThree;
    #endregion

    #region Unity-API
    private void Awake()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);

        _directionOne = Random.Range(0, 2) * 2 - 1;
        _directionTwo = Random.Range(0, 2) * 2 - 1;
        _directionThree = Random.Range(0, 2) * 2 - 1;

        RandomPositions();
    }

    private void Update()
    {
        if(active) return;

        RotateFragments();

        CheckWindCondition();
    }
    #endregion

    #region Logic
    public void ToggleRotationDirection(int circleIndex)
    {
        switch (circleIndex)
        {
            case 1:
                _directionOne *= -1;
                break;
            case 2:
                _directionTwo *= -1;
                break;
            case 3:
                _directionThree *= -1;
                break;
        }
    }

    private void CheckWindCondition()
    {
        if (Quaternion.Angle(_circleOne.rotation, _circleTwo.rotation) <= 10f &&
            Quaternion.Angle(_circleTwo.rotation, _circleThree.rotation) <= 10f &&
            Quaternion.Angle(_circleOne.rotation, _circleThree.rotation) <= 10f)
        {
            active = true;
            _circleOne.rotation = _circleThree.rotation;
            _circleTwo.rotation = _circleThree.rotation;
        }
    }

    private void RotateFragments()
    {
        _circleOne.Rotate(0, 0, _directionOne * _speed * Time.deltaTime);
        _circleTwo.Rotate(0, 0, _directionTwo * _speed * Time.deltaTime);
        _circleThree.Rotate(0, 0, _directionThree * _speed * Time.deltaTime);
    }

    private void RandomPositions()
    {
        _circleOne.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        _circleTwo.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        _circleThree.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
    }
    #endregion
}