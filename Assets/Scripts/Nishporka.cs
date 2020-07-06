using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nishporka : MonoBehaviour
{
    [SerializeField] private AudioClip _moveClip;
    [SerializeField] private AudioClip _seeClip;
    private bool _moveClipIsPlay = true;
    private bool _seeClipIsPlay = false;

    private static int _numberOfArtefacts = 0;
    private float _speed = 3.5f;
    private GameObject[] _positionsOfTheArtifactsGO;
    private List<Vector3> _positionsOfTheArtifacts;
    private Vector3 _targetPosition;
    private Vector3 _nishporkaTerritoryPosition;
    private RaycastHit _hit;
    private NavMeshAgent _agent;
    private GameObject _player = null;
    private GameObject _artefact = null;
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();

        _positionsOfTheArtifactsGO = GameObject.FindGameObjectsWithTag("LocationOfTheArtifact");
        _positionsOfTheArtifacts = new List<Vector3>();

        for (int i = 0; i < _positionsOfTheArtifactsGO.Length; i++)
        {
            _positionsOfTheArtifacts.Add(_positionsOfTheArtifactsGO[i].transform.position);
        }

        _agent = GetComponent<NavMeshAgent>();

        _agent.speed = _speed;
        _targetPosition = SetRandomTargetPosition(_positionsOfTheArtifacts);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_player == null && _artefact == null && IsApproximatlyEqualTo(this.transform.position, this._targetPosition))
        {
            _targetPosition = SetRandomTargetPosition(_positionsOfTheArtifacts);
        }
        else if(_player != null)
        {
            _targetPosition = _player.transform.position;
        }
        else if(_artefact != null)
        {
            _targetPosition = _artefact.transform.position;
        }

        if (!_audio.isPlaying && !_seeClipIsPlay)
        {
            _audio.PlayOneShot(_moveClip, 0.85f);
            _moveClipIsPlay = true;
        }

        _agent.SetDestination(_targetPosition);
    }
        
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Artefact")
        {
            Ray ray = new Ray(transform.position, other.gameObject.transform.position - transform.position);
            Physics.Raycast(ray, out _hit);
            Debug.DrawRay(transform.position, other.gameObject.transform.position - transform.position, Color.green, 0.1f);

            if (_hit.transform != null)
            {
                if (_hit.transform.tag == "Player")
                {
                    _player = other.gameObject;
                    if (_moveClipIsPlay)
                    {
                        _audio.Stop();
                        _moveClipIsPlay = false;
                    }
                    if (!_audio.isPlaying)
                    {
                        _audio.PlayOneShot(_seeClip, 1f);
                        _seeClipIsPlay = true;
                    }
                }
                else if (_hit.transform.tag == "Artefact")
                {
                    _artefact = other.gameObject;
                }

                Debug.Log("I see " + _hit.transform.name);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Artefact")
        {
            _NumberOfArtefacts++;

            collision.gameObject.SetActive(false);
            _artefact = null;
        }

        else if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Моя прелесть!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _player = null;
            _seeClipIsPlay = false;
        }
        else if (other.tag == "Artefact")
            _artefact = null;
    }

    private bool IsApproximatlyEqualTo(Vector3 tr1, Vector3 tr2, float accuracy = 0.75f)    //повертає true, якщо tr1 знаходиться на відстані accuracy від tr2 
    {
        bool res = false;
        accuracy = Mathf.Abs(accuracy);

        if ((tr1.x + accuracy >= tr2.x && tr1.z + accuracy >= tr2.z) && (tr1.x - accuracy <= tr2.x && tr1.z - accuracy <= tr2.z))
        {
            res = true;
        }

        return res;
    }

    private Vector3 SetRandomTargetPosition(List<Vector3> positions)
    {
        Vector3 pos = positions[Random.Range(0, positions.Count - 1)];
        return pos;
    }

    public static int _NumberOfArtefacts
    {
        get { return _numberOfArtefacts; }
        set
        {
            if (value >= 0) _numberOfArtefacts = value;
        }
    }
}
