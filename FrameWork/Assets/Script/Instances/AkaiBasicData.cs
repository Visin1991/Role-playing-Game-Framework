using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AkaiBasicData : EditorTimeBuildInSerializableData {

    public Vector3 pos;
    public float maxHealth = 100;
    public float currentHealth = 100;

    public float maxMana = 100;
    public float currentMana = 100;

    public override void SetUpDataType()
    {
        type = typeof(AkaiBasicData).FullName;
    }

    protected override void DeSerializeDataInternal(BinaryReader reader)
    {
        pos.x = reader.ReadSingle();
        pos.y = reader.ReadSingle();
        pos.z = reader.ReadSingle();

        maxHealth = reader.ReadSingle();
        currentHealth = reader.ReadSingle();
        maxMana = reader.ReadSingle();
        currentMana = reader.ReadSingle();

        transform.position = pos;
    }

    protected override void SerializeDataInternal(BinaryWriter writer)
    {
        //Write Pos
        pos = transform.position;
        writer.Write(pos.x);
        writer.Write(pos.y);
        writer.Write(pos.z);

        writer.Write(maxHealth);
        writer.Write(currentHealth);
        writer.Write(maxMana);
        writer.Write(currentMana);

    }
}
