using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
	[Header("Statistics Texts")]
	[SerializeField] private Text hungerText;
	[SerializeField] private Text tirednessText;
	[SerializeField] private Text boredomText;
	PlayerMovement player;

	[Header("Stats")]
	public float hunger = 100;
	public float tiredness = 100;
	public float boredom = 100;
	public float dirty = 5;

	[Header("Stat Drop Per Secound")]
	[Range(0.001f, 10)]
	[SerializeField] private float hungerFactor = 1;
	[Range(0.001f, 10)]
	[SerializeField] private float tirednessFactor = 1;
	[Range(0.001f, 10)]
	[SerializeField] private float boredomFactor = 1;

	public bool offTirednessFactor;
	[SerializeField] private FastMenu menu;

	[Header("Material")]
	[SerializeField] private MeshRenderer my_renderer;
	[SerializeField] private Skins[] skins;
    [System.Serializable]
	public class Skins
    {
		public Material[] skin = new Material[4];
    }



	void Start()
	{
        if (LoadSave())
        {
			hunger = PlayerPrefs.GetFloat("Hunger");
			tiredness = PlayerPrefs.GetFloat("Tiredness");
			boredom = PlayerPrefs.GetFloat("Boredom");
			dirty = PlayerPrefs.GetFloat("Dirty");

        }

		player = GetComponent<PlayerMovement>();
	}
	private void Update()
	{
        if (!menu.openMenu)
        {
			Hunger(0);
			if(!offTirednessFactor)
				Tiredness(0);
			Boredom(0);
        }
	}
	public void Dirty(float clear)
    {
		dirty = Mathf.Clamp(dirty + clear, 1, 8);
		if (dirty / 2 <= 1)
			my_renderer.material = skins[0].skin[0];
		else if (dirty / 2 <= 2)
			my_renderer.material = skins[0].skin[1];
		else if (dirty / 2 <= 3)
			my_renderer.material = skins[0].skin[2];
		else
			my_renderer.material = skins[0].skin[3];
	}
	public void Hunger(float kcal)
    {
		hunger = Mathf.Clamp(hunger + kcal - Time.deltaTime * hungerFactor, 0, 100);
		hungerText.text = $"{Mathf.Round(hunger)}";



		if (hunger == 0)
		{
			string starvation = "You Suck";
			player.Die(starvation);
		}
    }
	public void Tiredness(float energy)
	{
		tiredness = Mathf.Clamp(tiredness + energy - Time.deltaTime * tirednessFactor, 0, 100);
		tirednessText.text = $"{Mathf.Round(tiredness)}";

	}
	public void Boredom(float happiness)
	{
		boredom = Mathf.Clamp(boredom + happiness - Time.deltaTime * boredomFactor, 0, 100);
		boredomText.text = $"{Mathf.Round(boredom)}";

	}
	bool LoadSave()
    {
		if (PlayerPrefs.GetFloat("Hunger") != 0)
			return true;
		return false;
		
	}
}
