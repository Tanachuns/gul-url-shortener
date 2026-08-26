import React from "react";
import type { StatsResultData } from "~/types/StatusResultData";

type Props = {
  resultData: StatsResultData | null;
};

export default function StatsResult({ resultData }: Props) {
  if (!resultData) {
    return null;
  }

  console.log(resultData);

  return (
    <>
      <div className="divider divider-secondary"></div>
      <ul className="list bg-base-100 rounded-box shadow-md">
        <li className="p-4 pb-2 text-xs opacity-60 tracking-wide">
          Your URL Stats
        </li>

        <li className="list-row">
          <div>
            <div>Full URL</div>
            <a className="text-xs opacity-60" href={resultData.longUrl || "#"} target="_blank" rel="noopener noreferrer">
              {resultData.longUrl || "N/A"}
            </a>
          </div>
        </li>
        <li className="list-row">
          <div>
            <div>Active</div>
            <div className="text-xs opacity-60">
              {resultData.isActive ? "Yes" : "No"}
            </div>
          </div>
        </li>
        <li className="list-row">
          <div>
            <div>Last Accessed</div>
            <div className="text-xs opacity-60">
              {resultData.lastAccessed || "N/A"}
            </div>
          </div>
        </li>
         <li className="list-row">
          <div>
            <div>Visited</div>
            <div className="text-xs opacity-60">
              {resultData.visited || "N/A"}
            </div>
          </div>
        </li>
      </ul>
    </>
  );
}
