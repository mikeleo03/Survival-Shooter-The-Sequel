using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FaerieCircle : MonoBehaviour
{
    [System.Serializable]
    public struct FaerieMood
    {
        public Color mainFaerieColor;
        public Color mainCircleColor;
        public Color accentColor;
        public Color glowColor;
        public float glowIntensity;
        public float areaCost;
        public float windForce;
        public float speed;
        public float minimumTime;
    }

    public FaerieMood happyFaerie;
    public FaerieMood angryFaerie;

    private float faerieSpeed;
    public int grenadeStock = 1;
    public float cullRadius = 5f;

    private float radius = 1f;
    private ParticleSystem faerieParticles;
    private ParticleSystem circleParticles;
    private WindZone windZone;
    private int remainingGrenades;
    private Transform faerie;
    private Light faerieGlow;
    private Vector3 moveVector = Vector3.zero;
    public float moveTimer = 0f;
    private CullingGroup cullGroup;
    private FaerieAnger cameraEffect;

	void Start ()
    {
        PopulateParticleSystemCache();
        SetupStateBehaviours();
        SetupWind();
        SetupCullingGroup();

        faerieGlow = this.GetComponentInChildren<Light>();

        remainingGrenades = grenadeStock;
        faerieSpeed = happyFaerie.speed;
    }

    void OnEnable()
    {
        Camera camMain = Camera.main;
        cameraEffect = camMain.gameObject.GetComponent<FaerieAnger>();
    }

    private void SetupStateBehaviours()
    {
        Animator anim = this.gameObject.GetComponent<Animator>();
        FaerieStateBehaviour[] stateBehaviours = anim.GetBehaviours<FaerieStateBehaviour>();
        for (int i = 0; i < stateBehaviours.Length; i++)
        {
            stateBehaviours[i].Setup(this);
        }
    }

    private void PopulateParticleSystemCache()
    {
        ParticleSystem[] pSystems = this.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < pSystems.Length; i++)
        {
            ParticleSystem.MainModule pMain = pSystems[i].main;
            if (pSystems[i].shape.shapeType == ParticleSystemShapeType.Circle)
            {
                circleParticles = pSystems[i];
                radius = pSystems[i].shape.radius;
            }
            else
            {
                faerie = pSystems[i].gameObject.transform;
                faerieParticles = pSystems[i];
            }
        }
    }

    private void SetupWind()
    {
        windZone = this.GetComponentInChildren<WindZone>();
        windZone.windMain = happyFaerie.windForce;
    }

    private void SetupCullingGroup()
    {
        cullGroup = new CullingGroup();
        cullGroup.targetCamera = Camera.main;
        cullGroup.SetBoundingSpheres(new BoundingSphere[] { new BoundingSphere(transform.position, cullRadius) });
        cullGroup.SetBoundingSphereCount(1);
        cullGroup.onStateChanged += OnStateChanged;
    }

    void OnStateChanged(CullingGroupEvent cullEvent)
    {
        if (cullEvent.isVisible)
        {
            faerieParticles.Play(true);
        }
        else
        {
            faerieParticles.Pause();
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Enemy" && coll.attachedRigidbody.isKinematic)
        {
            MakeAngry();
        }
    }

    void Update()
    {
        if (moveTimer > 0f)
        {
            moveTimer -= Time.deltaTime;
            MoveFaerie(Time.deltaTime * moveVector);
        }
        else
        {
            moveTimer = faerieSpeed;
            moveVector = GetRandomVector();
        }
    }

    private void ActivateFaerie(bool activate)
    {
        GameObject faerieGO = faerie.gameObject;
        if (faerieGO.activeInHierarchy != activate)
        {
            faerieGO.SetActive(activate);
        }
    }

    public void SetMood(bool angry)
    {
        if (angry)
        {
            SetValuesFromMood(angryFaerie);
            cameraEffect.SetAnger(angryFaerie.minimumTime);
        }
        else
        {
            SpawnGrenade();
            SetValuesFromMood(happyFaerie);
        }
    }

    private void SetValuesFromMood(FaerieMood mood)
    {
        faerieSpeed = mood.speed;

        ColorParticle(faerieParticles, mood.mainFaerieColor, mood.accentColor);
        ColorParticle(circleParticles, mood.mainCircleColor, mood.accentColor);

        faerieGlow.color = mood.glowColor;
        faerieGlow.intensity = mood.glowIntensity;

        windZone.windMain = mood.windForce;
    }

    private void ColorParticle(ParticleSystem pSys, Color mainColor, Color accentColor)
    {
        ParticleSystem.MainModule pMain = pSys.main;
        pMain.startColor = new ParticleSystem.MinMaxGradient(mainColor, accentColor);
    }

    private void SpawnGrenade()
    {
        if (remainingGrenades < 1)
        {
            return;
        }
        remainingGrenades--;
        PoolManager.Pull("Grenade", this.transform.position, Quaternion.identity);
    }

    public void MakeAngry()
    {
        this.GetComponent<Animator>().SetInteger("Anger", 11);
    }

    private void MoveFaerie(Vector3 delta)
    {
        faerie.localPosition += delta;
    }

    private Vector3 GetRandomVector()
    {
        Vector3 randomPoint = UnityEngine.Random.insideUnitSphere * radius;
        randomPoint += radius * Vector3.up;
        return (randomPoint - faerie.localPosition) / faerieSpeed;
    }

    void OnDestroy()
    {
        if (cullGroup != null)
            cullGroup.Dispose();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, cullRadius);
    }
}
