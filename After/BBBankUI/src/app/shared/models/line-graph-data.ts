import { ApiResponse } from "src/app/model/api-Response"

export interface LineGraphData {
  totalBalance: number
  labels: string[]
  figures: number[]
}

export interface LineGraphDataResponse extends ApiResponse {
  result: LineGraphData
}