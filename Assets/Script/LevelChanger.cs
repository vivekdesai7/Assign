using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour {

	public Animator animator;

	private int levelToLoad;

	public GameObject[] mObject;
	public Material[] mMat;



	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			// Bit shift the index of the layer (8) to get a bit mask
			int layerMask = 1 << 8;

			RaycastHit hit;
			// Does the ray intersect any objects excluding the player layer
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
			{	
				Debug.Log("Did Hit----"+ hit.collider.gameObject.name);
				hit.collider.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();

                int i = 0;

				if (hit.collider.gameObject.name.Contains("Cube")){
					i = int.Parse(hit.collider.gameObject.name.Replace("Cube", ""));
				} else if (hit.collider.gameObject.name.Contains("Sphere")) {
					i = int.Parse(hit.collider.gameObject.name.Replace("Sphere", ""));
				} else if (hit.collider.gameObject.name.Contains("Cylinder")) {
					i = int.Parse(hit.collider.gameObject.name.Replace("Cylinder", ""));
				}



				switch (i)
                {
					case 1:
						mObject[0].transform.GetChild(1).GetComponent<TextMesh>().gameObject.SetActive(true);
						mObject[1].transform.GetChild(1).GetComponent<TextMesh>().gameObject.SetActive(false);
						mObject[2].transform.GetChild(1).GetComponent<TextMesh>().gameObject.SetActive(false);
						mObject[0].transform.GetComponent<MeshRenderer>().material = mMat[0];
						mObject[1].transform.GetComponent<MeshRenderer>().material = mMat[1];
						mObject[2].transform.GetComponent<MeshRenderer>().material = mMat[1];
						break;

					case 2:
						mObject[0].transform.GetChild(1).GetComponent<TextMesh>().gameObject.SetActive(false);
						mObject[1].transform.GetChild(1).GetComponent<TextMesh>().gameObject.SetActive(true);
						mObject[2].transform.GetChild(1).GetComponent<TextMesh>().gameObject.SetActive(false);
						mObject[0].transform.GetComponent<MeshRenderer>().material = mMat[1];
						mObject[1].transform.GetComponent<MeshRenderer>().material = mMat[0];
						mObject[2].transform.GetComponent<MeshRenderer>().material = mMat[1];
						break;

					case 3:
						mObject[0].transform.GetChild(1).GetComponent<TextMesh>().gameObject.SetActive(false);
						mObject[1].transform.GetChild(1).GetComponent<TextMesh>().gameObject.SetActive(false);
						mObject[2].transform.GetChild(1).GetComponent<TextMesh>().gameObject.SetActive(true);
						mObject[0].transform.GetComponent<MeshRenderer>().material = mMat[1];
						mObject[1].transform.GetComponent<MeshRenderer>().material = mMat[1];
						mObject[2].transform.GetComponent<MeshRenderer>().material = mMat[0];
						break;

					default:
                        break;
                }
            }
			
		}
	}


	public void FadeToNextLevel ()
	{
		Debug.Log("------" + SceneManager.GetActiveScene().buildIndex + 1);
		FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void FadeToHome()
	{
		FadeToLevel(0);
	}

	public void FadeToLevel (int levelIndex)
	{
		levelToLoad = levelIndex;
		animator.SetTrigger("FadeOut");
	}

	public void OnFadeComplete ()
	{
		SceneManager.LoadScene(levelToLoad);
	}
}
