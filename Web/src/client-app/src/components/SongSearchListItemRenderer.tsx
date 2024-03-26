import React from "react";
import { ISongSearchListItem } from "../SongSearchList";
import SongSearchItem from "./SongSearchItem";

interface ISongSearchListItemRendererProps {
  item: ISongSearchListItem;
  q?: string;
}

function SongSearchListItemRenderer({ item, q }: ISongSearchListItemRendererProps) {
  return <SongSearchItem item={item} q={q} />;
}

export default React.memo(SongSearchListItemRenderer);