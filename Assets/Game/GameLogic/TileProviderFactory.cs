﻿using System;
using System.Collections.Generic;
public static class TileProviderFactory
{
    private static Func<ITile, ITile, bool> isEmpty = (origin, observable) => observable.Unit == null;
    private static Func<ITile, ITile, bool> isEnemy = (origin, observable) => observable.Unit != null;
    public static TileProvider ForMove(int moves)
    {
        return new TileProvider(moves, TileProvider.ProviderMode.Blocking, isEmpty);
    }

    public static TileProvider ForMeleeAttack()
    {
        return new TileProvider(2, TileProvider.ProviderMode.ThroughAll, isEnemy);
    }
}