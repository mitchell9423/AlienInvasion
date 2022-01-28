using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LootCrate))]
public class LootCrateKeyController : MonoBehaviour {

	public KeyCode openKey = KeyCode.Space;
	public KeyCode closeKey = KeyCode.Space;

	private LootCrate _lootCrate;
    private Save_Script _save;

    bool empty = false;

    private void Awake()
	{
		_lootCrate = GetComponent<LootCrate>();
        _save = Save_Script.Instance;
	}

	private void Update()
	{
		if (_lootCrate.IsOpeningOrClosing()) return;

        if (!empty)
        {
            if (Input.GetKeyDown(openKey) && _lootCrate.IsClosed()) _lootCrate.Open();
            if (Input.GetKeyDown(closeKey) && _lootCrate.IsOpen())
            {
                _lootCrate.Close();
                empty = true;
                _save.nanites += UnityEngine.Random.Range(50, 301);
                Invoke("trash", 4.0f);
            }
        }
	}

    private void trash()
    {
        Destroy(gameObject);
    }
}
